using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Models;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Navigation;

namespace Yugen.Toolkit.Uwp.Samples.Constants
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
