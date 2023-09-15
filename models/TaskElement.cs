using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace models
{
    public record TaskElement
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("class_name")]
        public string Class_Name { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
        [BsonElement("valid_until")]
        public DateTime UntilDate { get; set; }   
    }
}
