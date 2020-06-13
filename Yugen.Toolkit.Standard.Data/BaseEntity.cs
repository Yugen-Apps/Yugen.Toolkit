using System;

namespace Yugen.Toolkit.Standard.Data
{
    /// <summary>
    /// We're going to add a primary key of type Guid in each model 
    /// manually, because as best practice is better to have a proper 
    /// naming like {ModelName}Id instead of just Id
    /// <code>
    /// public Guid Id { get; set; }
    /// </code>
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Created
        /// </summary>
        public DateTimeOffset Created { get; set; }
        /// <summary>
        /// LastUpdated
        /// </summary>
        public DateTimeOffset LastUpdated { get; set; }
        /// <summary>
        /// ClientLastUpdated
        /// </summary>
        public DateTimeOffset ClientLastUpdated { get; set; }
    }
}