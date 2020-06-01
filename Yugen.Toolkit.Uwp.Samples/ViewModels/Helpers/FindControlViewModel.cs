using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Helpers;
using Yugen.Toolkit.Uwp.Mvvm.ComponentModel;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Helpers
{
    public class FindControlViewModel : ViewModelBase
    {
        private string _title = "Find Control Page";
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public void FindAncestorStackPanelButton_Click(object sender, RoutedEventArgs _)
        {
            var dependencyObject = FindControlHelper.FindAncestor<StackPanel>(sender);
            ShowResult(dependencyObject);
        }

        public void FindAncestorPageButton_Click(object sender, RoutedEventArgs _)
        {
            var dependencyObject = FindControlHelper.FindAncestor<Page>(sender);
            ShowResult(dependencyObject);
        }        
        
        public void FindAncestorStackPanelByNameButton_Click(object sender, RoutedEventArgs _)
        {
            var dependencyObject = FindControlHelper.FindAncestor<StackPanel>(sender, "myStackPanel");
            ShowResult(dependencyObject);
        }

        public void FindDescendantButton_Click(object sender, RoutedEventArgs _)
        {
            var page = FindControlHelper.FindAncestor<Page>(sender);
            var dependencyObject = FindControlHelper.FindDescendant<Button>(page);
            ShowResult(dependencyObject);
        }

        public void FindDescendantByNameButton_Click(object sender, RoutedEventArgs _)
        {
            var page = FindControlHelper.FindAncestor<Page>(sender);
            var dependencyObject = FindControlHelper.FindDescendant<Button>(page, "MyButton");
            ShowResult(dependencyObject);
        }
                
        public async void FindButtons_Click(object sender, RoutedEventArgs _)
        {
            var page = FindControlHelper.FindAncestor<Page>(sender);
            var controlList = FindControlHelper.GetControlList<Button>(page);
            await MessageDialogHelper.Alert(controlList.Count.ToString());
        }

        private async void ShowResult(DependencyObject dependencyObject)
        {
            var type = dependencyObject?.GetType()?.ToString() ?? "Not Found";
            await MessageDialogHelper.Alert(type);
        }
    }
}