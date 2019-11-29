using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Models;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Helpers;
using Yugen.Toolkit.Uwp.Samples.Views.Navigation;

namespace Yugen.Toolkit.Uwp.Samples.Constants
{
    public static class Menu
    {
        public static List<MenuItem> MenuCollection = new List<MenuItem>() 
        {
            HomeMenu,
            ControlsMenu,
            HelpersMenu,
            NavigationMenu
        };

        public static MenuItem HomeMenu => new MenuItem("Home", nameof(HomePage));

        public static MenuItem ControlsMenu => new MenuItem
        {
            Name = "Controls",
            IsExpanded = false,
            Children =
            {
                new MenuItem ("Custom Dialog", nameof (CustomDialogPage)),
                new MenuItem ("Graph", nameof (GraphPage)),
                new MenuItem ("Notification Banner", nameof (NotificationBannerPage)),
                new MenuItem ("Validation", nameof (ValidationPage))
            }
        };

        public static MenuItem HelpersMenu => new MenuItem
        {
            Name = "Helpers",
            IsExpanded = false,
            Children =
            {
                new MenuItem ("File Picker", nameof (FilePickerPage)),
                new MenuItem ("Find Control", nameof (FindControlPage))
            }
        };

        public static MenuItem NavigationMenu => new MenuItem
        {
            Name = "Navigation",
            IsExpanded = false,
            Children = 
            {
                new MenuItem ("Basic Navigation", nameof (NavigationPage))
            }
        };
    }
}