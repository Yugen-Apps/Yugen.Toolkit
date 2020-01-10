using System;
using Yugen.Toolkit.Standard.Data.Interfaces;

namespace Yugen.Toolkit.Standard.Data.Models
{
    public class BaseEntity : IBaseEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        //public int Id { get; set; }

        public int Index { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastUpdated { get; set; }

        public DateTimeOffset ClientLastUpdated { get; set; }
    }
}