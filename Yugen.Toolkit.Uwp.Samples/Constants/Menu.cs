using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Collections;
using Yugen.Toolkit.Uwp.Samples.Views.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Helpers;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Converters;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.MediaCompositionNS;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Win2D;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Data;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Mvvm;
using FontIcon = Windows.UI.Xaml.Controls.FontIcon;

namespace Yugen.Toolkit.Uwp.Samples.Constants
{
    public static class MenuConstants
    {
        public static List<NavigationViewItemBase> NavItems = new List<NavigationViewItemBase>
        {
            NewItem("Home", nameof(HomePage)),
            NewHeader("Yugen Toolkit"),
            NewSubMenu("Collections",new List<NavigationViewItem>
            {
                NewItem ("Grouped Collection", nameof (GroupedCollectionPage)),
                NewItem ("Stretched Listview Item", nameof (StretchedCollectionPage))
            }),
            NewSubMenu("Controls",new List<NavigationViewItem>
            {
                NewItem ("Custom Dialog", nameof (CustomDialogPage)),
                NewItem ("EdgeTapped ListView", nameof (EdgeTappedListViewPage)),
                NewItem ("Graph", nameof (GraphPage)),
                NewItem ("Notification Banner", nameof (NotificationBannerPage)),
                NewItem ("Validation", nameof (ValidationPage)),
                NewItem ("Sample In App Control", nameof (SampleInAppControlPage)),
                NewItem ("Yugen Dialog", nameof (YugenDialogPage)),
            }),
            NewSubMenu("Data",new List<NavigationViewItem>
            {
                NewItem ("CRUD", nameof (DataPage)),
            }),
            NewSubMenu("Helpers",new List<NavigationViewItem>
            {
                NewItem ("Content Dialog", nameof (ContentDialogPage)),
                NewItem ("File Picker", nameof (FilePickerPage)),
                NewItem ("Find Control", nameof (FindControlPage))
            }),
            NewSubMenu("MVVM",new List<NavigationViewItem>
            {
                NewItem ("Observable Settings", nameof (ObservableSettingsPage))
            }),
            NewHeader("Microsoft Toolkit"),
            NewSubMenu("MVVM",new List<NavigationViewItem>
            {
                NewItem ("Command", nameof (CommandPage)),
                NewItem ("Mediator ", nameof (MediatorPage)),
                NewItem ("Navigation Parameters", nameof (NavigationPage)),
                NewItem ("Observable Object ", nameof (ObservableObjectPage)),
                NewItem ("XamlUICommandPage", nameof (XamlUICommandPage))
            }),
            NewHeader("Snippets"),
            NewSubMenu("Converters",new List<NavigationViewItem>
            {
                NewItem ("Enum To Boolean", nameof (EnumToBooleanConverterPage))
            }),
            NewSubMenu("MediaComposition",new List<NavigationViewItem>
            {
                NewItem ("Demo", nameof (MediaCompositionPage))
            }),
            NewSubMenu("MVVM",new List<NavigationViewItem>
            {
                NewItem ("Xaml ViewModel", nameof (XamlViewModelPage))
            }),
            NewSubMenu("Win2D",new List<NavigationViewItem>
            {
                NewItem ("Loading Wave", nameof (LoadingWavePage))
            })
        };

        public static NavigationViewItemHeader NewHeader(string content) =>
            new NavigationViewItemHeader { Content = content };

        public static NavigationViewItem NewSubMenu(string content, List<NavigationViewItem> menuItemsSource) =>
            new NavigationViewItem
            {
                Content = content,
                Icon = new FontIcon { Glyph = "\uEA37" },
                IsExpanded = false,
                SelectsOnInvoked = false,
                MenuItemsSource = menuItemsSource
            };

        public static NavigationViewItem NewItem(string content, string tag) =>
            new NavigationViewItem
            {
                Content = content,
                Tag = tag,
                Icon = new FontIcon { Glyph = "\uE80F" },
                IsExpanded = true,
                SelectsOnInvoked = true
            };
    }
}