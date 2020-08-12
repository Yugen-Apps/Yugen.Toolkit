using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples
{
    public sealed partial class AppShell : Page
    {
        private readonly ObservableCollection<MenuItem> MenuCollection = new ObservableCollection<MenuItem>(Menu.MenuList);

        public AppShell()
        {
            InitializeComponent();
        }

        private void MenuTree_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var tag = args.InvokedItemContainer.Tag?.ToString();
            if(tag != null)
                NavigationService.NavigateToPage(tag);
        }
    }
}
