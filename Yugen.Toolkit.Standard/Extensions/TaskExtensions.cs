using System;
using System.Threading.Tasks;
using Yugen.Toolkit.Standard.Helpers;

namespace Yugen.Toolkit.Standard.Extensions
{
    public static class TaskExtensions
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLine(task.GetType(), ex);
            }
        }
    }
}
