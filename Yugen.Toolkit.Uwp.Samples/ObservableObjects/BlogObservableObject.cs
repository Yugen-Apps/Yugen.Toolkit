using System;
using System.Collections.Generic;
using Yugen.Toolkit.Standard.Data.Sample.Models;
using Yugen.Toolkit.Standard.Mvvm.ComponentModel;
using Yugen.Toolkit.Uwp.Samples.Comparers;

namespace Yugen.Toolkit.Uwp.Samples.ObservableObjects
{
    public class BlogObservableObject : ObservableObject<Blog>, IComparable<BlogObservableObject>
    {
        public BlogObservableObject(Blog model = null) : base(model)
        {
        }

        public Guid BlogId
        {
            get => Model.BlogId;
            set => SetProperty(Model.BlogId, value, (v) => Model.BlogId = v);
        }

        public string Url
        {
            get => Model.Url;
            set => SetProperty(Model.Url, value, (v) => Model.Url = v);
        }

        public List<Post> Posts
        {
            get => Model.Posts;
            //set => SetProperty(Model.Posts, value, (v) => Model.Posts = v);
        }

        int IComparable<BlogObservableObject>.CompareTo(BlogObservableObject next) =>
            new BlogObservableObjectComparer().Compare(this, next);
    }
}