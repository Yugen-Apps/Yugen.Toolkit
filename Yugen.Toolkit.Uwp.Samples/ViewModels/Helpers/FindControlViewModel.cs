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

        private async void ShowResult(DependencyObject dependencyObject)
        {
            var type = dependencyObject?.GetType()?.ToString() ?? "Not Found";
            await MessageDialogHelper.Alert(type);
        }
    }
}