using System;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Interfaces
{
    public interface INavigationService
    {
        Frame Frame { get; set; }
        string CurrentPageKey { get; }

        void NavigateTo(string valuesPage, object parameter = null);

        void Configure(string mainPage, Type type);
    }
}