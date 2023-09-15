using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace models
{
    public record News
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; } 
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("body")]
        public string? Body { get; set; }
        [BsonElement("image")]
        public string? ImageUrl { get; set; }  //url
    }
}
