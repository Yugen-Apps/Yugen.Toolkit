using System.Collections.Generic;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Snippets.Xaml
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomizationPage : Windows.UI.Xaml.Controls.Page
    {
        public CustomizationPage()
        {
            this.InitializeComponent();
        }

        public NamedColor NamedColor1 { get; } = new NamedColor("q", "w", true);
        public NamedColor NamedColor2 { get; } = new NamedColor("a","s", false);
        public NamedColor NamedColor3 { get; } = new NamedColor("z","x", true);

        public List<NamedColor> NamedColorList { get; } = new List<NamedColor>
        {
            new NamedColor("q", "w", true),
            new NamedColor("a","s", false),
            new NamedColor("z","x", true)
        };

        public List<MyNavigationViewItem> NamedColorList1 { get; } = new List<MyNavigationViewItem>
        {
            new MyNavigationViewItem("q", "w", Visibility.Visible),
            new MyNavigationViewItem("a","s", Visibility.Collapsed),
            new MyNavigationViewItem("z","x", Visibility.Visible, true)
        };

        public List<MyNavigationViewItem2> NamedColorList2 { get; } = new List<MyNavigationViewItem2>
        {
            new MyNavigationViewItem2("q", "w", Visibility.Visible),
            new MyNavigationViewItem2("a","s", Visibility.Collapsed),
            new MyNavigationViewItem2("z","x", Visibility.Visible, true)
        };
    }

    public class NamedColor
    {
        public NamedColor(string colorName, string colorValue, bool isVisible)
        {
            Name = colorName;
            Color = colorValue;
            IsVisible = isVisible;
        }

        public string Name { get; set; }

        public string Color { get; set; }

        public bool IsVisible { get; set; }
    }

    public class MyNavigationViewItem : Windows.UI.Xaml.Controls.NavigationViewItem
    {
        public MyNavigationViewItem(string colorName, string colorValue, Visibility visibility, bool isSelected = false)
        {
            MyName = colorName;
            Color = colorValue;
            Visibility = visibility;
            Content = colorName;
            IsSelected = isSelected;
        }

        public string MyName { get; set; }


        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(nameof(Color), typeof(string), typeof(MyNavigationViewItem), new PropertyMetadata("sss"));
    }

    public class MyNavigationViewItem2 : NavigationViewItem
    {
        public MyNavigationViewItem2(string colorName, string colorValue, Visibility visibility, bool isSelected = false)
        {
            MyName = colorName;
            Color = colorValue;
            Visibility = visibility;
            Content = colorName;
            IsSelected = isSelected;
        }

        public string MyName { get; set; }


        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(nameof(Color), typeof(string), typeof(MyNavigationViewItem), new PropertyMetadata("sss"));
    }

    public static class NavigationViewItemAttachedProperty
    {
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsVisible", typeof(bool),
                typeof(NavigationViewItem), new PropertyMetadata(true, IsVisibleChangedCallback));

        public static void SetIsVisible(DependencyObject element, bool value) =>
            element.SetValue(IsVisibleProperty, value);

        public static bool GetIsVisible(DependencyObject element) =>
            (bool)element.GetValue(IsVisibleProperty);

        private static void IsVisibleChangedCallback(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is NavigationViewItem fb)
            {
                if ((bool)e.NewValue)
                {
                    fb.Visibility = Visibility.Visible;
                }
                else
                {
                    fb.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
