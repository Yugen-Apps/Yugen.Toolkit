using System.Linq;
using Windows.ApplicationModel;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class SystemHelper
    {
        /// <summary>
        /// Get Current App Version
        /// </summary>
        public static string AppVersion =>  $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";

        /// <summary>
        /// Get Publisher Display Name
        /// </summary>
        public static string Publisher => Package.Current.PublisherDisplayName;

        /// <summary>
        /// Get computer name.
        /// </summary>
        public static string HostName => NetworkInformation.GetHostNames()
                                             .FirstOrDefault(name => name.Type == HostNameType.DomainName)
                                             ?.DisplayName ?? "";

        /// <summary>
        /// Return true if it's a mobile device
        /// </summary>
        public static bool IsMobile => Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";

        /// <summary>
        /// Get RateAndReview store Url
        /// </summary>
        public static string RateAndReviewUri => $"ms-windows-store:REVIEW?PFN={Package.Current.Id.FamilyName}";
    }
}