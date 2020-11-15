using System.Collections.ObjectModel;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class EdgeTappedListViewPage : Page
    {
        public EdgeTappedListViewPage()
        {
            this.InitializeComponent();

            MyListView.ItemLeftEdgeTapped += MyListView_ItemLeftEdgeTapped;
        }

        private ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>
        {
            new Contact{FirstName="aaa", LastName="sss"},
            new Contact{FirstName="zzz", LastName="xxx"}
        };

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            // This is how devs can handle the back button
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        protected override void OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
        }

        private void MyListView_ItemLeftEdgeTapped(ListView arg1, Uwp.Controls.Collections.EdgeTappedListViewEventArgs arg2)
        {
            // When user releases the pointer after pressing on the left edge of the item,
            // the ListView will switch to Multiple Selection
            MyListView.SelectionMode = ListViewSelectionMode.Multiple;

            // Also, we want the Left Edge Tap funcionality will be no longer enable.
            MyListView.IsItemLeftEdgeTapEnabled = false;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            // We want to exit from the multiselect mode when pressing back button
            if (!e.Handled && MyListView.SelectionMode == ListViewSelectionMode.Multiple)
            {
                MyListView.SelectedItems.Clear();
                UpdateSelectionUI();
                e.Handled = true;
            }
        }

        private void OnSelectionChanged(object _1, SelectionChangedEventArgs _2)
        {
            UpdateSelectionUI();
        }

        private void UpdateSelectionUI()
        {
            // When there are no selected items, the list returns to None selection mode.
            if (MyListView.SelectedItems.Count == 0)
            {
                MyListView.SelectionMode = ListViewSelectionMode.None;
                MyListView.IsItemLeftEdgeTapEnabled = true;
            }
        }
    }
}