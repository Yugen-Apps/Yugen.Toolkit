using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Constants.Menu;
using Yugen.Toolkit.Uwp.Samples.Views.Yugen;
using Yugen.Toolkit.Uwp.Services;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels
{
    /// <summary>
    /// AppShellViewModel
    /// </summary>
    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel()
        {
            NavigationViewOnItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(NavigationViewOnItemInvokedCommandBehavior);
        }

        public IEnumerable<NavigationViewItemBase> NavItems => MenuBase.MenuList;

        public ICommand NavigationViewOnItemInvokedCommand { get; }

        private void NavigationViewOnItemInvokedCommandBehavior(NavigationViewItemInvokedEventArgs args)
        {
            var tag = args.InvokedItemContainer.Tag?.ToString();

            if (tag == "Settings")
            {
                NavigationService.NavigateToPage(nameof(SettingsPage));
            }
            else
            {
                NavigationService.NavigateToPage(tag);
            }
        }
    }
}