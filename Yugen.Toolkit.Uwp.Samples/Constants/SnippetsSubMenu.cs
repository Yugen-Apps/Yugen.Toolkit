using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Converters;
using Yugen.Toolkit.Uwp.Samples.Views.Snippets.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Constants
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
                Menu.NewNavigationViewItem ("Enum To Boolean", nameof (EnumToBooleanConverterPage))
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
                Menu.NewNavigationViewItem ("Xaml ViewModel", nameof (XamlViewModelPage))
            }
        };
    }
}