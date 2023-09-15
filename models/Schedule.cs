using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
namespace models
{
    public record Schedule {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("key")]
        public int key { get; set; }
        [BsonElement("body")]
        public List<ScheduleEvent> Body { get; set; }
    }

}
