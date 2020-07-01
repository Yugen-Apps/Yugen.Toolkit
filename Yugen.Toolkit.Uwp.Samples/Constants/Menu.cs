using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.Models;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Collections;
using Yugen.Toolkit.Uwp.Samples.Views.Controls;
using Yugen.Toolkit.Uwp.Samples.Views.Helpers;
using Yugen.Toolkit.Uwp.Samples.Views.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Constants
{
    public static class Menu
    {
        public static List<MenuItem> MenuCollection = new List<MenuItem>()
        {
            Home,
            Collections,
            Controls,
            Helpers,
            Mvvm
        };

        public static MenuItem Controls => new MenuItem
        {
            Name = nameof(Controls),
            IsExpanded = false,
            Children =
            {
                new MenuItem ("Custom Dialog", nameof (CustomDialogPage)),
                new MenuItem ("EdgeTapped ListView", nameof (EdgeTappedListViewPage)),
                new MenuItem ("Graph", nameof (GraphPage)),
                new MenuItem ("Notification Banner", nameof (NotificationBannerPage)),
                new MenuItem ("Validation", nameof (ValidationPage)),
                new MenuItem ("Sample In App Control", nameof (SampleInAppControlPage)),
            }
        };

        public static MenuItem Collections => new MenuItem
        {
            Name = nameof(Collections),
            IsExpanded = false,
            Children =
            {
                new MenuItem ("Grouped Collection", nameof (GroupedCollectionPage)),
                new MenuItem ("Stretched Listview Item", nameof (StretchedCollectionPage))
            }
        };

        public static MenuItem Helpers => new MenuItem
        {
            Name = nameof(Helpers),
            IsExpanded = false,
            Children =
            {
                new MenuItem ("Content Dialog", nameof (ContentDialogPage)),
                new MenuItem ("File Picker", nameof (FilePickerPage)),
                new MenuItem ("Find Control", nameof (FindControlPage))
            }
        };

        public static MenuItem Home => new MenuItem(nameof(Home), nameof(HomePage));

        public static MenuItem Mvvm => new MenuItem
        {
            Name = nameof(Mvvm),
            IsExpanded = false,
            Children =
            {
                new MenuItem ("Command", nameof (CommandPage)),
                new MenuItem ("Mediator ", nameof (MediatorPage)),
                new MenuItem ("Navigation Parameters", nameof (NavigationPage)),
                new MenuItem ("Observabl eObject ", nameof (ObservableObjectPage))
            }
        };
    }
}