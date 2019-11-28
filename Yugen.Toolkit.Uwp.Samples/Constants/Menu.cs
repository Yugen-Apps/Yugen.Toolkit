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
                new MenuItem ("Validation", nameof (ValidationPage)),
                new MenuItem ("Graph", nameof (GraphPage)),
                new MenuItem ("Custom Dialog", nameof (CustomDialogPage)),
                new MenuItem ("Notification Banner", nameof (NotificationBannerPage))
            }
        };

        public static MenuItem HelpersMenu => new MenuItem
        {
            Name = "Helpers",
            IsExpanded = false,
            Children = 
            {
                new MenuItem ("Find Control", nameof (FindControlPage)),
                new MenuItem ("File Picker", nameof (FilePickerPage))
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