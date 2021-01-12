using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Converters;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.MediaCompositionNS;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Win2D;

namespace Yugen.Toolkit.Uwp.Samples.Constants.Menu
{
    public static class SnippetsSubMenu
    {
        public static NavigationViewItem Converters => new NavigationViewItem
        {
            Content = nameof(Converters),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                MenuBase.NewNavigationViewItem ("Enum To Boolean", nameof (EnumToBooleanConverterPage))
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
                MenuBase.NewNavigationViewItem ("Xaml ViewModel", nameof (XamlViewModelPage))
            }
        };

        public static NavigationViewItem Win2D => new NavigationViewItem
        {
            Content = nameof(Win2D),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                MenuBase.NewNavigationViewItem ("Loading Wave", nameof (LoadingWavePage))
            }
        };

        public static NavigationViewItem MediaComposition => new NavigationViewItem
        {
            Content = nameof(MediaComposition),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                MenuBase.NewNavigationViewItem ("Demo", nameof (MediaCompositionPage))
            }
        };
    }
}