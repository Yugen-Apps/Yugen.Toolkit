using System;
using System.Collections.Generic;

namespace Yugen.Toolkit.Standard.Data.Sample.Models
{
    public class Blog : BaseEntity
    {
        public Guid BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
    }
}
