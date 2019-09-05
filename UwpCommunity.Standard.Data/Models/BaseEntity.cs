using System;
using UwpCommunity.Standard.Data.Interfaces;

namespace UwpCommunity.Standard.Data.Models
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