using System.Linq;
using Windows.ApplicationModel;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace Common.Uwp.Helpers
{
    public static class SystemHelper
    {
        public static string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        /// <summary>
        /// Get computer name.
        /// </summary>
        public static string HostName => NetworkInformation.GetHostNames()
                                             .FirstOrDefault(name => name.Type == HostNameType.DomainName)
                                             ?.DisplayName ?? "";

        public static bool IsMobile => Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";
    }
}