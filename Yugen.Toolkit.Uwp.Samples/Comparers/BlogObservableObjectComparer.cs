using System.Collections.Generic;
using Yugen.Toolkit.Uwp.Samples.ObservableObjects;

namespace Yugen.Toolkit.Uwp.Samples.Comparers
{
    public class BlogObservableObjectComparer : IComparer<BlogObservableObject>
    {
        public int Compare(BlogObservableObject x, BlogObservableObject y)
        {
            return x.Url.CompareTo(y.Url);
        }
    }
}