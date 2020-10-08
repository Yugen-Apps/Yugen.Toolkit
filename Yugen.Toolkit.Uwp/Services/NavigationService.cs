using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Yugen.Toolkit.Standard.Mvvm.Interfaces;

namespace Yugen.Toolkit.Uwp.Services
{
    public static class NavigationService
    {
        private static Assembly _appAssembly;
        private static Frame _rootFrame;
        private static Type _rootPage;

        public static void Initialize(Type app, Frame rootFrame, Type rootPage = null)
        {
            _appAssembly = app.GetTypeInfo().Assembly;
            _rootFrame = rootFrame;
            _rootPage = rootPage;

            _rootFrame.Navigating += async (s, e) =>
            {
                if (e.Cancel)
                    return;

                // allow the viewmodel to cancel navigation
                //e.Cancel = !NavigatingFrom(false);
                if (!e.Cancel)
                {
                    await NavigateFromAsync(false);
                }
            };

            _rootFrame.Navigated += (s, e) =>
            {
                NavigateTo(e.Parameter);
            };

            AddBackButton();
        }

        public static void AddBackButton()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        public static void RemoveBackButton()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= SystemNavigationManager_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private static void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            BackRequested(ref handled);
            e.Handled = handled;
        }

        public static void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.
            if (_rootFrame == null) 
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (!_rootFrame.CanGoBack || handled) 
                return;

            // If not, set the event to handled and go back to the previous page in the app.
            handled = true;
            _rootFrame.GoBack();
        }

        public static void GoBack()
        {
            if (_rootFrame == null) 
                return;

            if (_rootFrame.CanGoBack)
                _rootFrame.GoBack();
        }


        public static void NavigateToPage(string page, object parameter = null, bool force = false)
        {
            if (string.IsNullOrEmpty(page))
                return;

            var targetPage = _appAssembly.DefinedTypes.FirstOrDefault(t => t.Name == page);
            if (targetPage == null) 
                return;

            NavigateToPage(targetPage.AsType(), parameter, force);
        }

        public static void NavigateToPage(Type page, object parameter = null, bool force = false)
        {
            NavigateToPage(page, parameter, null, force);
        }

        public static void NavigateToPage(Type page, object parameter, NavigationTransitionInfo navigationTransitionInfo, bool force = false)
        {
            if (page == null) 
                return;

            if (!IsSamePage(page) || force)
            {
                if (navigationTransitionInfo == null)
                {
                    _rootFrame.Navigate(page, parameter);
                }
                else
                {
                    _rootFrame.Navigate(page, parameter, navigationTransitionInfo);
                }
            }
        }


        public static void NavigateToPageAndClearStack(Type page, object parameter = null, bool force = false)
        {
            NavigateToPage(page, parameter, null, force);

            ClearBackStackTillLevel(1);
        }

        public static void NavigateToPageAndRemoveLastFromStack(Type page, object parameter = null, bool force = false)
        {
            NavigateToPage(page, parameter, null, force);

            _rootFrame.BackStack.RemoveAt(0);
        }

        private static void ClearBackStackTillLevel(int level)
        {
            if (_rootFrame.BackStackDepth <= level) 
                return;

            for (int i = _rootFrame.BackStackDepth; i > level; i--)
                _rootFrame.BackStack.RemoveAt(i - 1);
        }

        private static bool IsSamePage(Type page)
        {
            if (_rootFrame.Content != null)
                return _rootFrame.Content.GetType() == page;

            return false;
        }


        public static void NavigateTo(object parameter)
        {
            if (_rootFrame.Content is Page page)
            {
                // call viewmodel
                if (page.DataContext is INavigable dataContext)
                {
                    dataContext.OnNavigatedTo(parameter, null);
                }
            }
        }

        // after navigate
        public static async Task NavigateFromAsync(bool suspending)
        {
            if (_rootFrame.Content is Page page)
            {
                // call viewmodel
                if (page.DataContext is INavigable dataContext)
                {
                    //var pageState = _rootFrame.CurrentSourcePageType.GetType();
                    await dataContext.OnNavigatedFromAsync(null, suspending);
                }
            }
        }

        // before navigate (cancellable)
        //public static bool NavigatingFrom(bool suspending)
        //{
        //    var page = _rootFrame.Content as Page;
        //    if (page != null)
        //    {
        //        var dataContext = page.DataContext as INavigable;
        //        if (dataContext != null)
        //        {
        //            var args = new NavigatingEventArgs
        //            {
        //                PageType = _rootFrame.CurrentSourcePageType,
        //                Parameter = _rootFrame.CurrentSourcePageTypeProperty,
        //                Suspending = suspending,
        //            };
        //            dataContext.OnNavigatingFrom(args);
        //            return !args.Cancel;
        //        }
        //    }
        //    return true;
        //}
    }
}