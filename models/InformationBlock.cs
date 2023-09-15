using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace models
{
    public record InformationBlock
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
    }
}
