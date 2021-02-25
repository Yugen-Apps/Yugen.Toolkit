using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Data.Sample.Models;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Data
{
    public class DataViewModel : ViewModelBase
    {
        private readonly IBlogService _blogService;
        private readonly ILogger<DataViewModel> _logger;

        private string _url;
        private BlogObservableObject _selectedBlog;

        public DataViewModel(IBlogService blogService, ILogger<DataViewModel> logger)
        {
            _blogService = blogService;
            _logger = logger;

            AddOrUpdateCommand = new RelayCommand(AddOrUpdateCommandBehavior, () => !string.IsNullOrEmpty(Url));
            DeleteCommand = new RelayCommand(DeleteCommandBehavior);
            LoadedCommand = new RelayCommand(LoadCommandBehavior);
            NewCommand = new RelayCommand(() => SelectedBlog = null);
        }

        public ObservableCollection<BlogObservableObject> BlogCollection { get; } = new ObservableCollection<BlogObservableObject>();

        public string Url
        {
            get => _url;
            set
            {
                if (SetProperty(ref _url, value))
                {
                    AddOrUpdateCommand?.NotifyCanExecuteChanged();
                }
            }
        }

        public BlogObservableObject SelectedBlog
        {
            get => _selectedBlog;
            set
            {
                if(SetProperty(ref _selectedBlog, value))
                {
                    Url = SelectedBlog?.Url ?? string.Empty;
                }
            }
        }

        public IRelayCommand AddOrUpdateCommand { get; }

        public IRelayCommand DeleteCommand { get; }

        public IRelayCommand LoadedCommand { get; }

        public IRelayCommand NewCommand { get; }

        private void AddOrUpdateCommandBehavior()
        {
            var blog = SelectedBlog ?? new BlogObservableObject();
            blog.Url = Url;
            var blogResult = _blogService.AddOrUpdate(blog);

            if (SelectedBlog == null && blogResult.IsSuccess)
            {
                _logger.LogDebug("added");
                BlogCollection.Add(blog);
            }
        }

        private void DeleteCommandBehavior()
        {
            if (SelectedBlog != null)
            {
                var blogResult = _blogService.Delete(SelectedBlog);

                if (blogResult.IsSuccess)
                {
                    _logger.LogDebug("deleted");
                    BlogCollection.Remove(SelectedBlog);
                    SelectedBlog = null;
                }
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
                    BlogCollection.Add(new BlogObservableObject(blog));
                }
            }
        }
    }
}