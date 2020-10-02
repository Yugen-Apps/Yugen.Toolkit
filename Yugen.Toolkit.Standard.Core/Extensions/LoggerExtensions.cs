using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace Yugen.Toolkit.Standard.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogExtDebug<T>(this ILogger<T> logger,
            [CallerMemberName] string memberName = "") =>
                logger?.LogDebug($"[{typeof(T)} / {memberName}]");

        public static void LogExtDebug<T>(this ILogger<T> logger, Exception exception,
            [CallerMemberName] string memberName = "") =>
                logger?.LogDebug(exception, $"[{typeof(T)} / {memberName}]");

        public static void LogExtDebug<T>(this ILogger<T> logger, object message,
            [CallerMemberName] string memberName = "") =>
                logger?.LogDebug($"[{typeof(T)} / {memberName}] {message}");


        public static void LogExtInformation<T>(this ILogger<T> logger,
            [CallerMemberName] string memberName = "") =>
                logger?.LogInformation($"[{typeof(T)} / {memberName}]");

        public static void LogExtInformation<T>(this ILogger<T> logger, Exception exception,
            [CallerMemberName] string memberName = "") =>
                logger?.LogInformation(exception, $"[{typeof(T)} / {memberName}]");

        public static void LogExtInformation<T>(this ILogger<T> logger, object message,
            [CallerMemberName] string memberName = "") =>
                logger?.LogInformation($"[{typeof(T)} / {memberName}] {message}");


        public static void LogExtError<T>(this ILogger<T> logger, Exception exception,
            [CallerMemberName] string memberName = "") =>
                logger?.LogError(exception, $"[{typeof(T)} / {memberName}]");

        public static void LogExtError<T>(this ILogger<T> logger, object message,
            [CallerMemberName] string memberName = "") =>
                logger?.LogError($"[{typeof(T)} / {memberName}] {message}");


        public static void LogExtCritical<T>(this ILogger<T> logger, Exception exception,
            [CallerMemberName] string memberName = "") =>
                logger?.LogCritical(exception, $"[{typeof(T)} / {memberName}]");

        public static void LogExtCritical<T>(this ILogger<T> logger, object message,
            [CallerMemberName] string memberName = "") =>
                logger?.LogCritical($"[{typeof(T)} / {memberName}] {message}");
    }
}