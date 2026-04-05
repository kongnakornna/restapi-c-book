using Jtech.Common.DataStore.Interface;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Jtech.Common;
using Jtech.Common.BusinessLogic.Identity;
using Jtech.Common.BusinessLogic.AutoNumber;

namespace TestCommon.Models
{
    public class Category : IEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string CategoryGroup { get; set; }

        [AutoNumber("SSO")]
        public string CategoryNo { get; set; }

        [AutoNumber("SO")]
        public string PONo { get; set; }

        [IdentityFor(true)]
        public DateTime createDate { get; set; }

        [IdentityFor(true)]
        public string createBy { get; set; }


        [IdentityFor(false)]
        public DateTime? modifyDate { get; set; } = null;

        [IdentityFor(false)]
        public string? modifyBy { get; set; } = null;
    }
}
