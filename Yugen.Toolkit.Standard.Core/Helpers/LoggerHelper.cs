using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Yugen.Toolkit.Standard.Core.Helpers
{
    /// <summary>
    /// LoggerHelper
    /// </summary>
    public static class LoggerHelper
    {
        /// <summary>
        /// WriteLine
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="exception"></param>
        /// <param name="caller"></param>
        public static void WriteLine(Type classType, Exception exception, [CallerMemberName] string caller = null) =>
            Debug.WriteLine($"{DateTime.Now.TimeOfDay} [{classType.Name}/{caller}] Exception: {exception}");

        /// <summary>
        /// WriteLine
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="text"></param>
        /// <param name="caller"></param>
        public static void WriteLine(Type classType, string text, [CallerMemberName] string caller = null) =>
            Debug.WriteLine($"{DateTime.Now.TimeOfDay} [{classType.Name}/{caller}]: {text}");
    }
}