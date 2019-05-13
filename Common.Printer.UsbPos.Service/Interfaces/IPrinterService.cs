using System.Collections.Generic;

namespace Common.Printer.UsbPos.Service.Interfaces
{
    public interface IPrinterService
    {
        void Print(List<string> bodyList);
    }
}