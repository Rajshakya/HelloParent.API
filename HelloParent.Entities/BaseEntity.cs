using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities
{
   public class SQLBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdatedAt { get; set; }
        public DateTime ? DeletedAt { get; set; }
    }
}
