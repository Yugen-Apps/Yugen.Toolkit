using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Yugen.Toolkit.Standard.Data.Sample.Interfaces;
using Yugen.Toolkit.Standard.Extensions;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Samples.ObservableObjects;

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
            _blogService = blogService ?? throw new ArgumentNullException(nameof(IBlogService)); 
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

                //BlogCollection.Add(blog);
                //BlogCollection.SortBy(x => x.Url);

                //BlogCollection.AddSorted(blog, x => x.Url);
                //BlogCollection.AddSorted(blog, new BlogObservableObjectComparer());
                BlogCollection.AddSorted(blog);
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

                var blogCollection = blogListResult.Value.Select(b => new BlogObservableObject(b));
                //blogCollection = blogCollection.OrderBy(x => x.Url);
                
                BlogCollection.AddRange(blogCollection);

                //BlogCollection.Sort(x => x.Url);
                //BlogCollection.Sort(new BlogObservableObjectComparer());
                BlogCollection.Sort();
            }
        }
    }
}