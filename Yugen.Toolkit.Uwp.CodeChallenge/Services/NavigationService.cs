using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Yugen.Toolkit.Uwp.CodeChallenge.Interfaces;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();

        public NavigationService()
        {
        }

        public Frame Frame { get; set; }

        public string CurrentPageKey => throw new NotImplementedException();

        public void NavigateTo(string pageKey, object parameter = null)
        {
            if (!_pagesByKey.ContainsKey(pageKey))
            {
                throw new ArgumentException($"No such page: {pageKey}. Did you forget to call NavigationService.Configure?", nameof(pageKey));
            }

            Frame.Navigate(_pagesByKey[pageKey], parameter, new SuppressNavigationTransitionInfo());
        }

        public void Configure(string key, Type type)
        {
            _pagesByKey[key] = type;
        }
    }
}