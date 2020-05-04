using System;
using System.Runtime.CompilerServices;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class LoggerHelper
    {
        public static void WriteLine(Type classType, Exception exception, [CallerMemberName] string caller = null) => System.Diagnostics.Debug.WriteLine($"{DateTime.Now.TimeOfDay} [{classType.Name}/{caller}] Exception: {exception}");

        public static void WriteLine(Type classType, string text, [CallerMemberName] string caller = null) => System.Diagnostics.Debug.WriteLine($"{DateTime.Now.TimeOfDay} [{classType.Name}/{caller}]: {text}");
    }
}