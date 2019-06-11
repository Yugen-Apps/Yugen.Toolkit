using System.Collections.Generic;

namespace UwpCommunity.Standard.Printer.UsbPos.Interfaces
{
    public interface IPrinterService
    {
        void Print(List<string> bodyList);
    }
}