using MongoDB.Bson.Serialization.Attributes;
using models;

namespace models
{
    public record TaskSynb 
    {
        [BsonId]
        public Guid TaskId { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("body")]
        public List<TaskElement> Tasks { get; set; }
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set;}
    }
}
