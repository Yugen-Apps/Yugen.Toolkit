using Windows.ApplicationModel.Core;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class TitleBarHelper
    {
        public static void ExtendToTitleBar()
        {
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }
    }
}
