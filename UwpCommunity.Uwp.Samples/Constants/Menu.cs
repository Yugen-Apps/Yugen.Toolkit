using UwpCommunity.Uwp.Samples.Models;
using System.Collections.Generic;
using UwpCommunity.Uwp.Samples.Views;
using UwpCommunity.Uwp.Samples.Views.Controls;
using UwpCommunity.Uwp.Samples.Views.Navigation;

namespace UwpCommunity.Uwp.Samples.Constants
{
    public static class Menu
    {
        public static List<MenuItem> MenuCollection = new List<MenuItem>()
        {
            HomeMenu,
            ControlsMenu,
            NavigationMenu
        };

        public static MenuItem HomeMenu => new MenuItem
        {
            Name = "Home",
            Tag = nameof(HomePage)
        };

        public static MenuItem ControlsMenu => new MenuItem
        {
            Name = "Controls",
            IsExpanded = false,
            Children =
            {
                new MenuItem
                {
                    Name = "Validation",
                    Tag = nameof(ValidationPage)
                },
                new MenuItem
                {
                    Name = "Graph",
                    Tag = nameof(GraphPage)
                },
                new MenuItem
                {
                    Name = "Custom Dialog",
                    Tag = nameof(CustomDialogPage)
                },
                new MenuItem
                {
                    Name = "Notification Banner",
                    Tag = nameof(NotificationBannerPage)
                }
            }
        };

        public static MenuItem NavigationMenu => new MenuItem
        {
            Name = "Navigation",
            IsExpanded = false,
            Children =
            {
                new MenuItem
                {
                    Name = "Basic Navigation",
                    Tag = nameof(NavigationPage)
                }
            }                      
        };
    }
}
