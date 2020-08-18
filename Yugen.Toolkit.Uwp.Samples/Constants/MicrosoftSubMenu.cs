using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Constants
{
    public static class MicrosoftSubMenu
    {
        public static NavigationViewItem Mvvm => new NavigationViewItem
        {
            Content = nameof(Mvvm),
            Icon = new Windows.UI.Xaml.Controls.FontIcon { Glyph = "\uEA37" },
            IsExpanded = false,
            SelectsOnInvoked = false,
            MenuItemsSource = new List<NavigationViewItem>
            {
                Menu.NewNavigationViewItem ("Command", nameof (CommandPage)),
                Menu.NewNavigationViewItem ("Mediator ", nameof (MediatorPage)),
                Menu.NewNavigationViewItem ("Navigation Parameters", nameof (NavigationPage)),
                Menu.NewNavigationViewItem ("Observable Object ", nameof (ObservableObjectPage)),
                Menu.NewNavigationViewItem ("XamlUICommandPage", nameof (XamlUICommandPage))
            }
        };
    }
}