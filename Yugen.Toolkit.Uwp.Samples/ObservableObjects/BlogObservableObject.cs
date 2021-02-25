using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data.Sample.Models;
using Yugen.Toolkit.Standard.Mvvm.ComponentModel;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Navigation
{
    public class BlogObservableObject : ObservableObject<Blog>
    {
        public BlogObservableObject(Blog model = null) : base(model)
        {
        }

        public Guid BlogId
        {
            get => Model.BlogId;
            set => SetProperty(Model.BlogId, value, (v) => Model.BlogId = value);
        }

        public string Url
        {
            get => Model.Url;
            set => SetProperty(Model.Url, value, (v) => Model.Url = value);
        }

        public List<Post> Posts
        {
            get => Model.Posts;
            //set => SetProperty(Model.Posts, value, (v) => Model.Posts = value);
        }
    }
}