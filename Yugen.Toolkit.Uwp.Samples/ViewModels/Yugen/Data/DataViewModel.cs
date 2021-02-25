using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;
using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Data
{
    public class DataViewModel : ViewModelBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<DataViewModel> _logger;

        private string _url;

        public DataViewModel(IBlogService blogService, ILogger<DataViewModel> logger)
        {
            _blogService = blogService;
            _logger = logger;

            AddCommand = new RelayCommand(AddCommandBehavior, () => !string.IsNullOrEmpty(Url));
            LoadedCommand = new RelayCommand(LoadCommandBehavior);
        }

        public ObservableCollection<Blog> BlogCollection { get; } = new ObservableCollection<Blog>();

        public string Url
        {
            get => _url;
            set
            {
                if (SetProperty(ref _url, value))
                {
                    AddCommand?.NotifyCanExecuteChanged();
                }
            }
        }

        public IRelayCommand AddCommand { get; }

        public IRelayCommand LoadedCommand { get; }

        private void AddCommandBehavior()
        {
            var blog = new Blog { Url = Url };
            var result = _blogService.Add(blog);

            if (result.IsSuccess)
            {
                _logger.LogDebug("added");
                BlogCollection.Add(blog);
            }
        }

        private void LoadCommandBehavior()
        {
            var blogListResult = _blogService.Get();

            if (blogListResult.IsSuccess)
            {
                _logger.LogDebug($"list: {blogListResult.Value.Count()}");
                foreach (var blog in blogListResult.Value)
                {
                    BlogCollection.Add(blog);
                }
            }
        }
    }
}