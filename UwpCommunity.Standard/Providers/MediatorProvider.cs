using System;
using UwpCommunity.Standard.Helpers;
using UwpCommunity.Standard.Services;

namespace UwpCommunity.Standard.Providers
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