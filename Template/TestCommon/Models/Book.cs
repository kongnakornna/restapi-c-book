using Jtech.Common;
using Jtech.Common.Brokers.Base;
using Jtech.Common.DataStore;
using Jtech.Common.DataStore.Interface;
using Jtech.Common.BusinessLogic.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace TestCommon.Models
{
    public class Book:IEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string BookName { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public string Author { get; set; } = null!;

        [IdentityFor(true)]
        public DateTime createDate { get; set; }

        [IdentityFor(true)]
        public string createBy { get; set; }
    }
}
