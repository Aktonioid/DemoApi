using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using models;

namespace models
{
    public record MaterialsGroup
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("files")]
        public List<MaterialsFile>? Files { get; set; }
    }
}
