using System;
using Windows.Devices.Input;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Yugen.Toolkit.Uwp.Controls.Collections
{
    public class EdgeTappedListView : ListView
    {
        private const double HitTarget = 32;
        private const double VisualIndicatorWidth = 12;
        private const string VisualIndicatorName = "VisualIndicator";

        private ListViewItem _listViewItemHighlighted;
        private Rectangle _visualIndicator;

        /// <summary>
        /// This is event that will be fired when user releases the pointer after
        /// pressing on the left edge of the ListViewItem
        /// </summary>
        public event Action<ListView, EdgeTappedListViewEventArgs> ItemLeftEdgeTapped;
        //public delegate void ListViewEdgeTappedEventHandler(ListView sender, EdgeTappedListViewEventArgs e);
        //public event ListViewEdgeTappedEventHandler ItemLeftEdgeTapped;

        public Brush LeftEdgeBrush { get; set; }

        /// <summary>
        /// With this property we enable that the left edge tap visual indicator shows 
        /// when user press the listviewitem left edge 
        /// and also the ItemLeftEdgeTapped event will be fired 
        /// when user releases the pointer
        /// </summary>
        public bool IsItemLeftEdgeTapEnabled { get; set; } = true;

        public EdgeTappedListView()
        {
            ContainerContentChanging += OnContainerContentChanging;
            _listViewItemHighlighted = null;
            LeftEdgeBrush = Application.Current.Resources["SystemControlHighlightAltListAccentLowBrush"] as Brush;
        }

        private void OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                if (!(args.ItemContainer is ListViewItem lvi)) return;
                if (!(VisualTreeHelper.GetChild(lvi, 0) is UIElement element)) return;

                element.PointerPressed -= OnPointerPressed;
                element.PointerReleased -= OnPointerReleased;
                element.PointerCaptureLost -= OnPointerCaptureLost;
                element.PointerExited -= OnPointerExited;
            }
            else if (args.Phase == 0)
            {
                if (!(args.ItemContainer is ListViewItem lvi)) return;
                if (!(VisualTreeHelper.GetChild(lvi, 0) is UIElement element)) return;

                element.PointerPressed += OnPointerPressed;
                element.PointerReleased += OnPointerReleased;
                element.PointerCaptureLost += OnPointerCaptureLost;
                element.PointerExited += OnPointerExited;
            }
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (!IsItemLeftEdgeTapEnabled) return;
            // This conditional was commented to enable this on non-Mobile devices.
            // var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            // if (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile")
            
            if (e.Pointer.PointerDeviceType != PointerDeviceType.Touch) return;
            if (!(sender is UIElement element)) return;

            PointerPoint pointerPoint = e.GetCurrentPoint(element);
            if (!(pointerPoint.Position.X <= HitTarget)) return;

            _listViewItemHighlighted = VisualTreeHelper.GetParent(element) as ListViewItem;
            ShowVisual();
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (_listViewItemHighlighted != null)
                ClearVisual();
        }

        private void OnPointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            ClearVisual();
        }

        private void OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            ClearVisual();
        }

        private void ShowVisual()
        {
            if (_listViewItemHighlighted == null) return;
            _visualIndicator = _listViewItemHighlighted.FindName("VISUAL_INDICATOR_NAME") as Rectangle;

            if (_visualIndicator == null)
            {
                _visualIndicator = new Rectangle
                {
                    Name = VisualIndicatorName,
                    Height = _listViewItemHighlighted.ActualHeight,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = VisualIndicatorWidth,
                    Fill = LeftEdgeBrush,
                    Margin = new Thickness(-(_listViewItemHighlighted.Padding.Left), 0, 0, 0)
                };

                if (!(_listViewItemHighlighted.ContentTemplateRoot is Panel panel)) return;

                if (panel is Grid)
                    _visualIndicator.SetValue(Grid.RowSpanProperty, (panel as Grid).RowDefinitions.Count);

                panel.Children.Add(_visualIndicator);
            }
            else
            {
                _visualIndicator.Opacity = 1;
            }
        }

        private void ClearVisual()
        {
            if (_listViewItemHighlighted == null) return;

            if (_visualIndicator != null)
            {
                _visualIndicator.Opacity = 0;
                ItemLeftEdgeTapped?.Invoke(this, new EdgeTappedListViewEventArgs(_listViewItemHighlighted));
            }

            _listViewItemHighlighted = null;
        }
    }
}