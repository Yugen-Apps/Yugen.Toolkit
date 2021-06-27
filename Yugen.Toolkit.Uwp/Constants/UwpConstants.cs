namespace Yugen.Toolkit.Uwp.Constants
{
    public static class UwpConstants
    {
        /// <summary>
        /// access a file in the app package.
        /// </summary>
        public static string MsAppPackage => "ms-appx://";

        /// <summary>
        /// access a web file in the app package.
        /// </summary>
        public static string MsAppWebPackage => "ms-appx-web://";

        /// <summary>
        /// access a file in the app local, roaming, and temporary data folders.
        /// ms-appdata:///local/
        /// ms-appdata:///temp/
        /// ms-appdata:///roaming/
        /// </summary>
        public static string MsAppData => "ms-appdata://";


        public static string FolderAssets => "Assets";
    }
}
