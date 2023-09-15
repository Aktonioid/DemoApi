using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace models
{
    public record MaterialsFile
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }
    }
}
