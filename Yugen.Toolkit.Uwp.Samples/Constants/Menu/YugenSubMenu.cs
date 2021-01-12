using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views.Collections;
using Yugen.Toolkit.Uwp.Samples.Views.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Helpers;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Constants.Menu
{
    public static class YugenSubMenu
    {
        public static NavigationViewItem Collections => new NavigationViewItem
        {
            Content = nameof(Collections),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                MenuBase.NewNavigationViewItem ("Grouped Collection", nameof (GroupedCollectionPage)),
                MenuBase.NewNavigationViewItem ("Stretched Listview Item", nameof (StretchedCollectionPage))
            }
        };

        public static NavigationViewItem Controls => new NavigationViewItem
        {
            Content = nameof(Controls),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                MenuBase.NewNavigationViewItem ("Custom Dialog", nameof (CustomDialogPage)),
                MenuBase.NewNavigationViewItem ("EdgeTapped ListView", nameof (EdgeTappedListViewPage)),
                MenuBase.NewNavigationViewItem ("Graph", nameof (GraphPage)),
                MenuBase.NewNavigationViewItem ("Notification Banner", nameof (NotificationBannerPage)),
                MenuBase.NewNavigationViewItem ("Validation", nameof (ValidationPage)),
                MenuBase.NewNavigationViewItem ("Sample In App Control", nameof (SampleInAppControlPage)),
                MenuBase.NewNavigationViewItem ("Yugen Dialog", nameof (YugenDialogPage)),
            }
        };

        public static NavigationViewItem Helpers => new NavigationViewItem
        {
            Content = nameof(Helpers),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                MenuBase.NewNavigationViewItem ("Content Dialog", nameof (ContentDialogPage)),
                MenuBase.NewNavigationViewItem ("File Picker", nameof (FilePickerPage)),
                MenuBase.NewNavigationViewItem ("Find Control", nameof (FindControlPage))
            }
        };

        public static NavigationViewItem Mvvm => new NavigationViewItem
        {
            Content = nameof(Mvvm),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                MenuBase.NewNavigationViewItem ("Observable Settings", nameof (ObservableSettingsPage))
            }
        };
    }
}