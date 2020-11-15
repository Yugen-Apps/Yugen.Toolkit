using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views.Collections;
using Yugen.Toolkit.Uwp.Samples.Views.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Helpers;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Constants
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
                Menu.NewNavigationViewItem ("Grouped Collection", nameof (GroupedCollectionPage)),
                Menu.NewNavigationViewItem ("Stretched Listview Item", nameof (StretchedCollectionPage))
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
                Menu.NewNavigationViewItem ("Custom Dialog", nameof (CustomDialogPage)),
                Menu.NewNavigationViewItem ("EdgeTapped ListView", nameof (EdgeTappedListViewPage)),
                Menu.NewNavigationViewItem ("Graph", nameof (GraphPage)),
                Menu.NewNavigationViewItem ("Notification Banner", nameof (NotificationBannerPage)),
                Menu.NewNavigationViewItem ("Validation", nameof (ValidationPage)),
                Menu.NewNavigationViewItem ("Sample In App Control", nameof (SampleInAppControlPage)),
                Menu.NewNavigationViewItem ("Yugen Dialog", nameof (YugenDialogPage)),
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
                Menu.NewNavigationViewItem ("Content Dialog", nameof (ContentDialogPage)),
                Menu.NewNavigationViewItem ("File Picker", nameof (FilePickerPage)),
                Menu.NewNavigationViewItem ("Find Control", nameof (FindControlPage))
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
                Menu.NewNavigationViewItem ("Observable Settings", nameof (ObservableSettingsPage))
            }
        };
    }
}