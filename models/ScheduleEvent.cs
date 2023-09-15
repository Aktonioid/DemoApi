using MongoDB.Bson.Serialization.Attributes;
namespace models
{
    public record ScheduleEvent
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("fromtime")]
        public int FromTime
        { get; set; }
        [BsonElement("totime")]
        public int ToTime { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("kind")]
        public ScheduleEventKind Kind { get; set; }

    }

}
