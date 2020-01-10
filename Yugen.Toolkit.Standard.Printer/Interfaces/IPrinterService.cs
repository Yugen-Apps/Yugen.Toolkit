using System.Collections.Generic;

namespace Yugen.Toolkit.Standard.Printer.Interfaces
{
    public interface IPrinterService
    {
        void Print(List<string> bodyList);
    }
}