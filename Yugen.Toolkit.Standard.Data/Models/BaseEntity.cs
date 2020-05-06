using System;
using Yugen.Toolkit.Standard.Data.Interfaces;

namespace Yugen.Toolkit.Standard.Data.Models
{
    public class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// We're going to add a primary key of type Guid in each model 
        /// manually, because as best practice is better to have a proper 
        /// naming like {ModelName}Id instead of just Id
        /// </summary>
        // public Guid Id { get; set; }

        public int Index { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastUpdated { get; set; }

        public DateTimeOffset ClientLastUpdated { get; set; }
    }
}