using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Constants.Menu
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
                MenuBase.NewNavigationViewItem ("Command", nameof (CommandPage)),
                MenuBase.NewNavigationViewItem ("Mediator ", nameof (MediatorPage)),
                MenuBase.NewNavigationViewItem ("Navigation Parameters", nameof (NavigationPage)),
                MenuBase.NewNavigationViewItem ("Observable Object ", nameof (ObservableObjectPage)),
                MenuBase.NewNavigationViewItem ("XamlUICommandPage", nameof (XamlUICommandPage))
            }
        };
    }
}