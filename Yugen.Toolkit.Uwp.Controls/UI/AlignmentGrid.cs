using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Yugen.Toolkit.Standard.Helpers;

namespace Yugen.Toolkit.Uwp.Controls.UI
{
    /// <summary>
    /// AlignmentGrid is used to display a grid to help aligning controls
    /// </summary>
    public class AlignmentGrid : ContentControl
    {
        /// <summary>
        /// Identifies the <see cref="LineBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.Register(nameof(LineBrush), typeof(Brush),
                typeof(AlignmentGrid), new PropertyMetadata(null, OnPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="HorizontalStep"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalStepProperty =
            DependencyProperty.Register(nameof(HorizontalStep), typeof(double),
                typeof(AlignmentGrid), new PropertyMetadata(20.0, OnPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="VerticalStep"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalStepProperty =
            DependencyProperty.Register(nameof(VerticalStep), typeof(double),
                typeof(AlignmentGrid), new PropertyMetadata(20.0, OnPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="ContainerWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContainerWidthProperty =
            DependencyProperty.Register(nameof(ContainerWidth), typeof(double),
                typeof(AlignmentGrid), new PropertyMetadata(20.0, OnPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="ContainerHeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContainerHeightProperty =
            DependencyProperty.Register(nameof(ContainerHeight), typeof(double),
                typeof(AlignmentGrid), new PropertyMetadata(20.0, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var alignmentGrid = dependencyObject as AlignmentGrid;

            alignmentGrid?.Rebuild();
        }

        private readonly Canvas containerCanvas = new Canvas();

        /// <summary>
        /// Gets or sets the step to use horizontally.
        /// </summary>
        public Brush LineBrush
        {
            get => (Brush)GetValue(LineBrushProperty);
            set => SetValue(LineBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the step to use horizontally.
        /// </summary>
        public double HorizontalStep
        {
            get => (double)GetValue(HorizontalStepProperty);
            set => SetValue(HorizontalStepProperty, value);
        }

        /// <summary>
        /// Gets or sets the step to use horizontally.
        /// </summary>
        public double VerticalStep
        {
            get => (double)GetValue(VerticalStepProperty);
            set => SetValue(VerticalStepProperty, value);
        }

        /// <summary>
        /// Get or set the container width.
        /// </summary>
        public double ContainerWidth
        {
            get => (double)GetValue(ContainerWidthProperty);
            set => SetValue(ContainerWidthProperty, value);
        }

        /// <summary>
        /// Get or set the container height.
        /// </summary>
        public double ContainerHeight
        {
            get => (double)GetValue(ContainerHeightProperty);
            set => SetValue(ContainerHeightProperty, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlignmentGrid"/> class.
        /// </summary>
        public AlignmentGrid()
        {
            SizeChanged += AlignmentGrid_SizeChanged;

            IsHitTestVisible = false;
            IsTabStop = false;
            Opacity = 0.5;

            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            Content = containerCanvas;
        }

        private void Rebuild()
        {
            containerCanvas.Children.Clear();
            var horizontalStep = HorizontalStep;
            var verticalStep = VerticalStep;
            Brush brush = LineBrush ?? (Brush)Application.Current.Resources["ApplicationForegroundThemeBrush"];

            if (horizontalStep > 0)
            {
                for (double x = 0; x < ContainerWidth; x += HorizontalStep)
                {
                    var line = new Rectangle
                    {
                        Width = 1,
                        Height = ActualHeight,
                        Fill = brush
                    };
                    Canvas.SetLeft(line, MathHelper.RangeConvert(x, 0, ContainerWidth, 0, ActualWidth));

                    containerCanvas.Children.Add(line);
                }
            }

            if (verticalStep > 0)
            {
                for (double y = 0; y < ContainerHeight; y += VerticalStep)
                {
                    var line = new Rectangle
                    {
                        Width = ActualWidth,
                        Height = 1,
                        Fill = brush
                    };
                    Canvas.SetTop(line, MathHelper.RangeConvert(y, 0, ContainerHeight, 0, ActualHeight));

                    containerCanvas.Children.Add(line);
                }
            }
        }

        private void AlignmentGrid_SizeChanged(object sender, SizeChangedEventArgs e) => Rebuild();
    }
}