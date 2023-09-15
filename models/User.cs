using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace models
{
    public record User
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("login")]
        public string? Login { get; set; }
        [BsonElement("password")]
        public string? Password { get; set; }
        [BsonElement("kind")]
        public Kind Kind { get; set; }
        [BsonElement("first_name")]
        public string? first_name { get; set; }  
        [BsonElement("last_name")]  
        public string? last_Name { get; set;}
    }

    public enum Kind 
    {
        User,
        Admin
    }
}
