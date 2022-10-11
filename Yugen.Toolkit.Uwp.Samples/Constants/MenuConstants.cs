using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Audio.Samples.Views;
using Yugen.Toolkit.Standard.Helpers;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Microsoft.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Sandbox.Csharp;
using Yugen.Toolkit.Uwp.Samples.Views.Sandbox.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Sandbox.Xaml;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Converters;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Csharp;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.DragAndDrop;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.MediaCompositionNS;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Win2D;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Xaml;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Audio;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Collections;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Data;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen.Helpers;
using FontIcon = Windows.UI.Xaml.Controls.FontIcon;

namespace Yugen.Toolkit.Uwp.Samples.Constants
{
    public static class MenuConstants
    {
        public static List<NavigationViewItemBase> NavItems = new List<NavigationViewItemBase>
        {
            NewItem(nameof(HomePage)),

            NewHeader("Microsoft Toolkit"),
            NewSubMenu("MVVM",new List<NavigationViewItem>
            {
                NewItem (nameof (CommandPage)),
                NewItem (nameof (MediatorPage)),
                NewItem (nameof (NavigationPage)),
                NewItem (nameof (ObservableObjectPage)),
                NewItem (nameof (XamlUICommandPage))
            }),

            NewHeader("Snippets"),
            NewSubMenu("Converters",new List<NavigationViewItem>
            {
                NewItem (nameof (EnumToBooleanConverterPage))
            }),
            NewSubMenu("C#",new List<NavigationViewItem>
            {
                NewItem (nameof (TasksPage))
            }),
            NewSubMenu("DragAndDrop",new List<NavigationViewItem>
            {
                NewItem (nameof (DragAndDropCanvasPage)),
                NewItem (nameof (DragAndDropGridPage))
            }),
            NewSubMenu("MediaComposition",new List<NavigationViewItem>
            {
                NewItem (nameof (MediaCompositionPage))
            }),
            NewSubMenu("MVVM",new List<NavigationViewItem>
            {
                NewItem (nameof (XamlViewModelPage))
            }),
            NewSubMenu("Win2D",new List<NavigationViewItem>
            {
                NewItem (nameof (LoadingWavePage))
            }),
            NewSubMenu("Xaml",new List<NavigationViewItem>
            {
                NewItem (nameof (CustomizationPage)),
                NewItem (nameof (StylesPage))
            }),

            NewHeader("Sandbox"),
            NewSubMenu("C#",new List<NavigationViewItem>
            {
                NewItem (nameof (DeferralPage)),
                NewItem (nameof (ImagesGridPage)),
                NewItem (nameof (PlaygroundPage))
            }),
            NewSubMenu("MVVM",new List<NavigationViewItem>
            {
                NewItem (nameof (ObservableSettingsPage))
            }),
            NewSubMenu("Xaml",new List<NavigationViewItem>
            {
                NewItem (nameof (RsodPage))
            }),

            NewHeader("Yugen Toolkit"),
            NewSubMenu("Audio",new List<NavigationViewItem>
            {
                NewItem(nameof(AudioGraphPage)),
                NewItem(nameof(BassPage)),
                NewItem(nameof(CsCorePage)),
                NewItem(nameof(SharpDXPage)),
                NewItem(nameof(AudioFrameInputNodePage)),
                NewItem(nameof(WaveformPage)),
                NewItem(nameof(VinylPage)),
                NewItem(nameof(DeckPage)),
                NewItem(nameof(VuBarPage)),
                NewItem(nameof(LoopbackAudioCapturePage))
            }),
            NewSubMenu("Collections",new List<NavigationViewItem>
            {
                NewItem (nameof (GroupedCollectionPage)),
                NewItem (nameof (StretchedCollectionPage))
            }),
            NewSubMenu("Controls",new List<NavigationViewItem>
            {
                NewItem (nameof (CustomDialogPage)),
                NewItem (nameof (EdgeTappedListViewPage)),
                NewItem (nameof (GraphPage)),
                NewItem (nameof (NotificationBannerPage)),
                NewItem (nameof (ValidationPage)),
                NewItem (nameof (SampleInAppControlPage)),
                NewItem (nameof (YugenDialogPage)),
            }),
            NewSubMenu("Data",new List<NavigationViewItem>
            {
                NewItem (nameof (DataPage)),
            }),
            NewSubMenu("Helpers",new List<NavigationViewItem>
            {
                NewItem (nameof (ContentDialogPage)),
                NewItem (nameof (FilePickerPage)),
                NewItem (nameof (FindControlPage))
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

        public static NavigationViewItem NewItem(string tag) =>
            new NavigationViewItem
            {
                Content = StringHelper.SplitCamelCase(tag.Replace("Page", string.Empty)),
                Tag = tag,
                Icon = new FontIcon { Glyph = "\uE80F" },
                IsExpanded = true,
                SelectsOnInvoked = true
            };
    }
}