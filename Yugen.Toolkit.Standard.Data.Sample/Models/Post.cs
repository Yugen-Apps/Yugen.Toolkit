using System;

namespace Yugen.Toolkit.Standard.Data.Sample.Models
{
    public class Post : BaseEntity
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
