using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Snippets.DragAndDrop
{
    public sealed partial class DragAndDropCanvasPage : Page
    {
        public DragAndDropCanvasPage()
        {
            this.InitializeComponent();
        }

        private void PanelDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private void PanelDrop(object sender, DragEventArgs e)
        {
            var point = e.GetPosition(MyCanvas);

            var uiElement = DragableGrid as UIElement;
            Canvas.SetLeft(uiElement, point.X);
            Canvas.SetTop(uiElement, point.Y);
            uiElement.Opacity = 1;
        }

        private void UiElementDragStarting(UIElement sender, DragStartingEventArgs args)
        {
            DragableGrid.Opacity = 0.5;
        }
    }
}