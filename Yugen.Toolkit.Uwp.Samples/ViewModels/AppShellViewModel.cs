using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Views;
using Yugen.Toolkit.Uwp.Samples.Views.Sandbox.Xaml;
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

        public IEnumerable<NavigationViewItemBase> NavItems => MenuConstants.NavItems;

        public IRelayCommand NavigationViewOnItemInvokedCommand { get; }

        private void NavigationViewOnItemInvokedCommandBehavior(NavigationViewItemInvokedEventArgs args)
        {
            var tag = args.InvokedItemContainer.Tag?.ToString();

            switch (tag)
            {
                case "Settings":
                    NavigationService.NavigateToPage(nameof(SettingsPage));
                    break;
                case nameof(RsodPage):
                    throw new Exception("Hello");
                default:
                    NavigationService.NavigateToPage(tag);
                    break;
            }
        }
    }
}