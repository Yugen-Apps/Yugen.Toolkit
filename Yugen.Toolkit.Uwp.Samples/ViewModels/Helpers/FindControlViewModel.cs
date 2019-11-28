using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Helpers;
using Yugen.Toolkit.Uwp.ViewModels;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Helpers
{
    public class FindControlViewModel : BaseViewModel
    {
        private string _title = "Find Control Page";
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public void FindAncestorStackPanelButton_Click(object sender, RoutedEventArgs e)
        {
            var dependencyObject = FindControlHelper.FindAncestor<StackPanel>(sender);
            ShowResult(dependencyObject);
        }

        public void FindAncestorPageButton_Click(object sender, RoutedEventArgs e)
        {
            var dependencyObject = FindControlHelper.FindAncestor<Page>(sender);
            ShowResult(dependencyObject);
        }        
        
        public void FindAncestorStackPanelByNameButton_Click(object sender, RoutedEventArgs e)
        {
            var dependencyObject = FindControlHelper.FindAncestor<StackPanel>(sender, "myStackPanel");
            ShowResult(dependencyObject);
        }

        public void FindDescendantButton_Click(object sender, RoutedEventArgs e)
        {
            var dependencyObject = FindControlHelper.FindAncestor<Page>(sender);
            var dependencyObject2 = FindControlHelper.FindDescendant<Button>(dependencyObject);
            ShowResult(dependencyObject2);
        }

        public void FindDescendantByNameButton_Click(object sender, RoutedEventArgs e)
        {
            var dependencyObject = FindControlHelper.FindAncestor<Page>(sender);
            var dependencyObject2 = FindControlHelper.FindDescendant<Button>(dependencyObject, "MyButton");
            ShowResult(dependencyObject2);
        }

        private async void ShowResult(DependencyObject dependencyObject)
        {
            var type = dependencyObject?.GetType()?.ToString() ?? "Not Found";
            await MessageDialogHelper.Alert(type);
        }
    }
}