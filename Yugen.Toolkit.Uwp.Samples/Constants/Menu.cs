using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Collections;
using Yugen.Toolkit.Uwp.Samples.Views.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Helpers;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Constants
{
    public static class Menu
    {
        public static List<NavigationViewItemBase> MenuList = new List<NavigationViewItemBase>
        {
            Home,
            new NavigationViewItemHeader { Content = "Yugen Toolkit" },
            Collections,
            Controls,
            Helpers,
            new NavigationViewItemHeader { Content = "Microsoft Toolkit" },
            Mvvm,
            new NavigationViewItemHeader { Content = "Snippets" }
        };

        public static NavigationViewItem Home => NewNavigationViewItem(nameof(Home), nameof(HomePage));

        public static NavigationViewItem Collections => new NavigationViewItem
        {
            Content = nameof(Collections),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                NewNavigationViewItem ("Grouped Collection", nameof (GroupedCollectionPage)),
                NewNavigationViewItem ("Stretched Listview Item", nameof (StretchedCollectionPage))
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
                NewNavigationViewItem ("Custom Dialog", nameof (CustomDialogPage)),
                NewNavigationViewItem ("EdgeTapped ListView", nameof (EdgeTappedListViewPage)),
                NewNavigationViewItem ("Graph", nameof (GraphPage)),
                NewNavigationViewItem ("Notification Banner", nameof (NotificationBannerPage)),
                NewNavigationViewItem ("Validation", nameof (ValidationPage)),
                NewNavigationViewItem ("Sample In App Control", nameof (SampleInAppControlPage)),
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
                NewNavigationViewItem ("Content Dialog", nameof (ContentDialogPage)),
                NewNavigationViewItem ("File Picker", nameof (FilePickerPage)),
                NewNavigationViewItem ("Find Control", nameof (FindControlPage))
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
                NewNavigationViewItem ("Command", nameof (CommandPage)),
                NewNavigationViewItem ("Mediator ", nameof (MediatorPage)),
                NewNavigationViewItem ("Navigation Parameters", nameof (NavigationPage)),
                NewNavigationViewItem ("Observable Object ", nameof (ObservableObjectPage)),
                NewNavigationViewItem ("XamlUICommandPage", nameof (XamlUICommandPage))
            }
        };

        public static NavigationViewItem NewNavigationViewItem(string content, string tag) => 
            new NavigationViewItem
            {
                Content = content,
                Tag = tag,
                Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uE80F" },
                IsExpanded = true,
                SelectsOnInvoked = true
            };
    }
}