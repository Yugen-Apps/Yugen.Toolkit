using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Yugen.Toolkit.Uwp.Behaviors
{
    public class ManipulationBothDirectionsBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty TargetTransformProperty =
            DependencyProperty.Register(nameof(TargetTransform), typeof(CompositeTransform), typeof(ManipulationBothDirectionsBehavior),
                new PropertyMetadata(null));

        public CompositeTransform TargetTransform
        {
            get => (CompositeTransform)GetValue(TargetTransformProperty);
            set => SetValue(TargetTransformProperty, value);
        }

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;

            if (AssociatedObject is UIElement uiElement)
            {
                uiElement.ManipulationDelta += OnUiManipulationDeltaChanged;
            }
        }

        public void Detach()
        {
            if (AssociatedObject is UIElement uiElement)
            {
                uiElement.ManipulationDelta -= OnUiManipulationDeltaChanged;
            }
        }

        private void OnUiManipulationDeltaChanged(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            TargetTransform.TranslateX += e.Delta.Translation.X;
            TargetTransform.TranslateY += e.Delta.Translation.Y;

            e.Handled = true;
        }
    }
}
