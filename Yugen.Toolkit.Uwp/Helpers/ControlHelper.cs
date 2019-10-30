using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class ControlHelper
    {
        public static DependencyObject FindChildControl<T>(DependencyObject control, string ctrlName)
        {
            var childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (var i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                // Not a framework element or is null
                if (!(child is FrameworkElement fe)) return null;

                if (child is T && fe.Name == ctrlName)
                {
                    // Found the control so return
                    return child;
                }

                // Not found it - search children
                DependencyObject nextLevel = FindChildControl<T>(child, ctrlName);
                if (nextLevel != null)
                    return nextLevel;
            }

            return null;
        }

        private static readonly List<Control> ControlList = new List<Control>();

        public static List<Control> GetControlList<T>(object uiElement, bool reset = true)
        {
            if(reset)
                ControlList.Clear();

            FindControl<T>(uiElement);
            return ControlList;
        }

        private static void FindControl<T>(object uiElement)
        {
            switch (uiElement)
            {
                case T _:
                    ControlList.Add((Control)uiElement);
                    break;
                case Panel _:
                    var uiElementAsCollection = (Panel)uiElement;
                    foreach (var element in uiElementAsCollection.Children)
                        FindControl<T>(element);
                    break;
                case UserControl _:
                    var uiElementAsUserControl = (UserControl)uiElement;
                    FindControl<T>(uiElementAsUserControl.Content);
                    break;
                case ContentControl _:
                    var uiElementAsContentControl = (ContentControl)uiElement;
                    FindControl<T>(uiElementAsContentControl.Content);
                    break;
            }
        }

        public static DependencyObject FindParentControl<T>(object sender)
        {
            var element = ((FrameworkElement)sender).Parent;
            return element.GetType().BaseType == typeof(T) ? element : FindParentControl<T>(element);
        }

        public static Control NextControl(object sender)
        {
            var index = ((Control)sender).TabIndex + 1;
            return ControlList.FirstOrDefault(x => x.TabIndex == index);
        }

        public static void GoToNextControl(object sender)
        {
            NextControl(sender)?.Focus(FocusState.Programmatic);
        }

        internal static bool IsButtonEnabled(Button button, KeyRoutedEventArgs e) => !SystemHelper.IsMobile && button.IsEnabled;

        /// <summary>
        /// No Mobile Anymore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="createButton"></param>
        /// <returns></returns>
        public static bool GoToNextControlIfMobileOrCheckEnterIfDesktop(object sender, KeyRoutedEventArgs e, Button createButton)
        {
            if (!e.Key.Equals(Windows.System.VirtualKey.Enter))
                return false;

            if (!SystemHelper.IsMobile)
                return IsButtonEnabled(createButton, e);

            GoToNextControl(sender);
            return false;
        }
    }
}