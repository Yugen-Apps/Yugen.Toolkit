using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Views;

namespace Yugen.Toolkit.Uwp.Samples.Constants
{
    public static class Menu
    {
        public static List<NavigationViewItemBase> MenuList = new List<NavigationViewItemBase>
        {
            Home,
            new NavigationViewItemHeader { Content = "Yugen Toolkit" },
            YugenSubMenu.Collections,
            YugenSubMenu.Controls,
            YugenSubMenu.Helpers,
            YugenSubMenu.Mvvm,
            new NavigationViewItemHeader { Content = "Microsoft Toolkit" },
            MicrosoftSubMenu.Mvvm,
            new NavigationViewItemHeader { Content = "Snippets" },
            SnippetsSubMenu.Converters,
            SnippetsSubMenu.Mvvm
        };

        public static NavigationViewItem Home => NewNavigationViewItem(nameof(Home), nameof(HomePage));

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