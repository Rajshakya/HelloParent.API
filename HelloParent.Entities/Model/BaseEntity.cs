using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.Model
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
    public interface ITrackable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
    [BsonIgnoreExtraElements]
    public abstract class BaseEntity : IEntity<ObjectId>, ITrackable
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is BaseEntity)
            {
                var parsed = obj as BaseEntity;
                return this.Id == parsed.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
