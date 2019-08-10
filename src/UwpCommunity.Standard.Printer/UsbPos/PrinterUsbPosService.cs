using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using UwpCommunity.Standard.Printer.UsbPos.Interfaces;

//https://gist.github.com/romeshniriella/19cfa44056f641421247
namespace UwpCommunity.Standard.Printer.UsbPos
{
    public class PrinterUsbPosService : IPrinterService
    {
        #region Constructor & properties

        private readonly string _printerHostName;

        public PrinterUsbPosService() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName">NetworkInformation.GetHostNames().FirstOrDefault(name => name.Type == HostNameType.DomainName)?.DisplayName</param>
        /// <param name="printerName"></param>
        public PrinterUsbPosService(string hostName, string printerName)
        {
            _printerHostName = string.Format($"\\\\{hostName}\\{printerName}");
        }

        #endregion Constructor & properties

        #region Printer core methods

        public void Print(List<string> bodyList)
        {
            byte[] document = GetDocument(bodyList);
            IntPtr printerHandle;

            var documentInfo = new NativeMethods.DOC_INFO_1
            {
                pDataType = "RAW",
                pDocName = "Receipt"
            };

            printerHandle = new IntPtr(0);

            if (NativeMethods.OpenPrinter(_printerHostName.Normalize(), out printerHandle, IntPtr.Zero))
            {
                if (NativeMethods.StartDocPrinter(printerHandle, 1, documentInfo))
                {
                    IntPtr unmanagedData;
                    var managedData = document;
                    unmanagedData = Marshal.AllocCoTaskMem(managedData.Length);
                    Marshal.Copy(managedData, 0, unmanagedData, managedData.Length);

                    if (NativeMethods.StartPagePrinter(printerHandle))
                    {
                        NativeMethods.WritePrinter(
                            printerHandle,
                            unmanagedData,
                            managedData.Length,
                            out int bytesWritten);
                        NativeMethods.EndPagePrinter(printerHandle);
                    }
                    else
                    {
                        throw new Win32Exception();
                    }

                    Marshal.FreeCoTaskMem(unmanagedData);

                    NativeMethods.EndDocPrinter(printerHandle);
                }
                else
                {
                    throw new Win32Exception();
                }

                NativeMethods.ClosePrinter(printerHandle);
            }
            else
            {
                throw new Win32Exception();
            }
        }

        private byte[] GetDocument(List<string> bodyList)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                // Reset the printer bws (NV images are not cleared)
                bw.Write(AsciiControlChars.Escape);
                bw.Write('@');

                AddReceipt(bw, bodyList);

                bw.Write("\r\n");
                bw.Write("\r\n");
                bw.Write("\r\n");
                bw.Write("\r\n");

                // Feed 3 vertical motion units and cut the paper with a 1 point cut
                bw.Write(AsciiControlChars.GroupSeparator);
                bw.Write('V');
                bw.Write((byte)66);
                bw.Write((byte)3);
                bw.Flush();

                return ms.ToArray();
            }
        }

        #endregion Printer core methods

        #region Print Content

        /// <summary>
        /// This is the method we print the receipt the way we want. Note the spaces. 
        /// Wasted a lot of paper on this to get it right.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="bodyList"></param>
        private void AddReceipt(BinaryWriter bw, List<string> bodyList)
        {
            foreach (var item in bodyList)
                bw.Write($"{item}\r\n");
        }

        #endregion Print content
    }
}