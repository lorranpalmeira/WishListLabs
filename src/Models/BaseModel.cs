using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace WishListLabs.Models
{
    public class BaseModel
    {

        [BsonId]
        private Guid Code { get; set; }

        public int Id { get; set; }
    }
}
