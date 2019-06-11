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
            //LearningMenu
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
                }
            }
        };

        public static MenuItem LearningMenu => new MenuItem
        {
            Name = "70-357 Developing Mobile Apps",
            IsExpanded = false,
            Children =
            {
                new MenuItem
                {
                    Name = "XAML",
                    Children =
                    {
                        new MenuItem
                        {
                            Name = "Page layout",
                            Children =
                            {
                                new MenuItem
                                {
                                    Name = "RelativePanel",
                                    Tag = "ValidationPage"
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}
