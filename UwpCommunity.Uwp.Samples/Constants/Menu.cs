using UwpCommunity.Uwp.Samples.Models;
using System.Collections.Generic;

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
            Tag = "HomePage"
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
                    Tag = "ValidationPage"
                },
                new MenuItem
                {
                    Name = "Graph",
                    Tag = "GraphPage"
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
                    Tag = "NavigationPage"
                }
            }                      
        };
    }
}
