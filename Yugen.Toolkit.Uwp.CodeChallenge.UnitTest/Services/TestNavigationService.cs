using System;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.CodeChallenge.Interfaces;

namespace Yugen.Toolkit.Uwp.CodeChallenge.UnitTest.Services
{
    public class TestNavigationService : INavigationService
    {
        public void GoBack()
        {
            CurrentPageKey = string.Empty;
        }

        public void NavigateTo(string pageKey)
        {
            CurrentPageKey = pageKey;
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            CurrentPageKey = pageKey;
        }

        public void Configure(string mainPage, Type type)
        {
            throw new NotImplementedException();
        }

        public string CurrentPageKey { get; set; }
        public Frame Frame { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
