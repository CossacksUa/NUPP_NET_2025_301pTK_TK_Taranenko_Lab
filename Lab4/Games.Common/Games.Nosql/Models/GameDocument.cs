using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Games.Nosql.Models
{
    public class GameDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("genre")]
        public string Genre { get; set; } = string.Empty;

        [BsonElement("price")]
        public double Price { get; set; }
    }
}
