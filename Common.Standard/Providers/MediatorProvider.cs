using System;
using Common.Standard.Helpers;
using Common.Standard.Services;

namespace Common.Standard.Providers
{
    /// <summary>
    /// MediatorProvider.UpdateContent(title, MediatorPropertiesEnum.AppShellTitle.ToString());
    /// </summary>
    public static class MediatorProvider
    {
        public static void UpdateContent<T>(T text, string properties)
        {
            try
            {
                MediatorService.Instance.NotifyColleagues(properties, text);
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(MediatorProvider), exception);
            }
        }
    }
}