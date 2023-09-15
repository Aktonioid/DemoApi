
using MongoDB.Bson.Serialization.Attributes;

namespace DemoApi.Repositories.Refresh
{
    public record RefreshModel
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("token")]
        public List<string> TokenList { get; set; }
    }
}
