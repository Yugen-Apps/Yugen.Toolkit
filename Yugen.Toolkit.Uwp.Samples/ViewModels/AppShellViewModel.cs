using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        public IEnumerable<NavigationViewItemBase> NavItems => Menu.MenuList;

        public AppShellViewModel()
        {
            NavigationViewOnItemInvokedCommand = new RelayCommand<Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs>(NavigationViewOnItemInvokedCommandBehavior);
        }

        public ICommand NavigationViewOnItemInvokedCommand { get; }

        private void NavigationViewOnItemInvokedCommandBehavior(NavigationViewItemInvokedEventArgs args)
        {
            var tag = args.InvokedItemContainer.Tag?.ToString();
            if (tag != null)
                NavigationService.NavigateToPage(tag);
        }
    }
}