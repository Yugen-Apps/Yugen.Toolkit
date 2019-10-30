using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.PointOfService;
using Windows.UI.Core;
using Yugen.Toolkit.Standard.Printer.Interfaces;

namespace Yugen.Toolkit.Uwp.Printer.EscPos
{
    public class PrinterEscPosService : IPrinterService
    {
        private PosPrinter _printer;
        private ClaimedPosPrinter _claimedPrinter;
        private readonly bool _isAnImportantTransaction = true;

        private void ResetTheScenarioState()
        {
            //Remove releasedevicerequested handler and dispose claimed printer object.
            if (_claimedPrinter != null)
            {
                _claimedPrinter.ReleaseDeviceRequested -= ClaimedPrinter_ReleaseDeviceRequested;
                _claimedPrinter.Dispose();
                _claimedPrinter = null;
            }

            _printer = null;
        }

        /// <summary>
        /// Default checkbox selection makes it an important transaction, hence we do not release claim when we get a release devicerequested event.
        /// </summary>
        private async void ClaimedPrinter_ReleaseDeviceRequested(ClaimedPosPrinter sender, PosPrinterReleaseDeviceRequestedEventArgs args)
        {
            if (_isAnImportantTransaction)
                await sender.RetainDeviceAsync();
            else
                ResetTheScenarioState();
        }

        /// <summary>
        /// Creates multiple tasks, first to find a receipt printer, then to claim the printer and then to enable and add releasedevicerequested event handler.
        /// </summary>
        private async void FindClaimEnable()
        {
            if (!await FindReceiptPrinter()) return;
            if (!await ClaimPrinter()) return;
            await EnableAsync();
        }

        /// <summary>
        /// GetDeviceSelector method returns the string needed to identify a PosPrinter. This is passed to FindAllAsync method to get the list of devices currently available and we connect the first device.
        /// </summary>
        private async Task<bool> FindReceiptPrinter()
        {
            if (_printer != null) return true;
            //rootPage.NotifyUser("Finding printer", NotifyType.StatusMessage);
            DeviceInformationCollection deviceCollection = await DeviceInformation.FindAllAsync(PosPrinter.GetDeviceSelector());

            if (deviceCollection != null && deviceCollection.Count > 0)
            {
                DeviceInformation deviceInfo = deviceCollection[0];
                _printer = await PosPrinter.FromIdAsync(deviceInfo.Id);
                if (_printer != null)
                {
                    if (_printer.Capabilities.Receipt.IsPrinterPresent)
                    {
                        //rootPage.NotifyUser("Got Printer with Device Id : " + printer.DeviceId, NotifyType.StatusMessage);
                        return true;
                    }
                }
                else
                {
                    //rootPage.NotifyUser("No Printer found", NotifyType.ErrorMessage);
                    return false;
                }
            }
            else
            {
                //rootPage.NotifyUser("No devices returned by FindAllAsync.", NotifyType.ErrorMessage);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Actual claim method task that claims the printer asynchronously
        /// </summary>
        private async Task<bool> ClaimPrinter()
        {
            if (_claimedPrinter != null) return true;

            _claimedPrinter = await _printer.ClaimPrinterAsync();
            if (_claimedPrinter != null)
            {
                _claimedPrinter.ReleaseDeviceRequested += ClaimedPrinter_ReleaseDeviceRequested;
                //rootPage.NotifyUser("Claimed Printer", NotifyType.StatusMessage);
            }
            else
            {
                //rootPage.NotifyUser("Claim Printer failed", NotifyType.ErrorMessage);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check to make sure we still have claim on printer
        /// </summary>
        private bool IsPrinterClaimed()
        {
            if (_printer == null)
            {
                //rootPage.NotifyUser("Need to find printer first", NotifyType.ErrorMessage);
                return false;
            }

            return _claimedPrinter?.Receipt != null;
        }
        
        /// <summary>
        /// Enable the claimed printer.
        /// </summary>
        private async Task<bool> EnableAsync()
        {
            if (_claimedPrinter == null)
            {
                //rootPage.NotifyUser("No Claimed Printer to enable", NotifyType.ErrorMessage);
                return false;
            }

            return await _claimedPrinter.EnableAsync();

            //rootPage.NotifyUser("Enabled printer.", NotifyType.StatusMessage);
        }

        /// <summary>
        /// Prints a sample receipt
        /// </summary>
        public void Print(List<string> bodyList)
        {
            //if (!IsPrinterClaimed())
            //    return false;

            FindClaimEnable();

            try
            {
                ReceiptPrintJob job = _claimedPrinter.Receipt.CreateJob();

                foreach (var item in bodyList)
                    job.PrintLine($"{item}");

                job.CutPaper();
                // execute print jobs 
                // Note that we actually execute "job" twice in order to print 
                // the statement for both the customer as well as the merchant. 

                //if (!await job.ExecuteAsync())
                //{
                //    rootPage.NotifyUser("Failed to print receipts.", NotifyType.ErrorMessage);
                //    return false;
                //}
                //rootPage.NotifyUser("Printed receipts.", NotifyType.StatusMessage);
                //return true;
            }
            finally
            {
                CoreWindow.GetForCurrentThread().PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
            }
        }  
    }
}

