using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class FindControlHelper
    {
        /// <summary>
        /// Find the first ancestor of a control by type or by type and name
        /// </summary>
        /// <typeparam name="T">control type</typeparam>
        /// <param name="frameworkElement">control</param>
        /// <param name="name">name</param>
        /// <returns>control</returns>
        public static DependencyObject FindAncestor<T>(FrameworkElement frameworkElement, string name = null)
        {
            var element = frameworkElement.Parent;
            return InternalFindAncestor<T>(element, name);
        }

        /// <summary>
        /// Find the first ancestor of a control by type or by type and name
        /// </summary>
        /// <typeparam name="T">control type</typeparam>
        /// <param name="frameworkElement">control</param>
        /// <param name="name">name</param>
        /// <returns>control</returns>
        public static DependencyObject FindAncestor<T>(object sender, string name = null)
        {
            var element = ((FrameworkElement)sender).Parent;
            return InternalFindAncestor<T>(element, name);
        }

        /// <summary>
        /// Find the first ancestor of a control by type or by type and name
        /// </summary>
        /// <typeparam name="T">control type</typeparam>
        /// <param name="frameworkElement">control</param>
        /// <param name="name">name</param>
        /// <returns>control</returns>
        public static DependencyObject FindAncestor<T>(DependencyObject dependencyObject, string name = null)
        {
            var element = VisualTreeHelper.GetParent(dependencyObject);
            return InternalFindAncestor<T>(element, name);
        }


        private static DependencyObject InternalFindAncestor<T>(DependencyObject dependencyObject, string name = null)
        {
            if (dependencyObject == null)
                return null;

            var type = dependencyObject.GetType();
            var baseType = type.BaseType;

            if (type == typeof(T) || baseType == typeof(T))
            {
                if (name == null)
                    return dependencyObject;
                else if (((FrameworkElement)dependencyObject).Name.Equals(name))
                    return dependencyObject;
            }

            var element = ((FrameworkElement)dependencyObject).Parent;
            return InternalFindAncestor<T>(element);
        }


        /// <summary>
        /// Find the first Descendant of a control by type or by type and name
        /// </summary>
        /// <typeparam name="T">control type</typeparam>
        /// <param name="frameworkElement">control</param>
        /// <param name="name">name</param>
        /// <returns>control</returns>
        public static DependencyObject FindDescendant<T>(DependencyObject control, string name = null)
        {
            var childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (var i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);

                // Not a framework element or is null
                if (!(child is FrameworkElement frameworkElement))
                    return null;

                // Found the control so return
                if (child is T)
                {
                    if (name == null)
                        return child;
                    else if (frameworkElement.Name.Equals(name))
                        return child;
                }

                // Not found it - search children
                DependencyObject nextLevel = FindDescendant<T>(child, name);
                if (nextLevel != null)
                    return nextLevel;
            }

            return null;
        }


        private static readonly List<Control> ControlList = new List<Control>();

        public static List<Control> GetControlList<T>(object uiElement, bool reset = true)
        {
            if (reset)
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

        public static Control NextControl(object sender)
        {
            var index = ((Control)sender).TabIndex + 1;
            return ControlList.FirstOrDefault(x => x.TabIndex == index);
        }

        public static void GoToNextControl(object sender)
        {
            NextControl(sender)?.Focus(FocusState.Programmatic);
        }


        /// <summary>
        /// No Mobile Anymore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool GoToNextControlIfMobileOrCheckEnterIfDesktop(object sender, KeyRoutedEventArgs e, Button button)
        {
            if (!e.Key.Equals(Windows.System.VirtualKey.Enter))
                return false;

            if (!SystemHelper.IsMobile)
                return IsButtonEnabled(button, e);

            GoToNextControl(sender);
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static bool IsButtonEnabled(Button button, KeyRoutedEventArgs e) => !SystemHelper.IsMobile && button.IsEnabled;
    }
}