using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
namespace Lab1_Game
{
    internal class DataBase
    {
        MongoClient dbClient;
        IMongoDatabase db;
        public DataBase() 
        {
            dbClient = new MongoClient("mongodb+srv://vektor19:0957422713@cluster1.xk6ovkq.mongodb.net/");
            db = dbClient.GetDatabase("Game");
        }
        public async Task<List<BsonDocument>> GetPlayers()
        {
            var playersCollection = db.GetCollection<BsonDocument>("Players");
            var filter = Builders<BsonDocument>.Filter.Empty;

            // Retrieve all players from the collection asynchronously
            var players = await playersCollection.Find(filter).ToListAsync();

            return players;
        }
        public async Task InsertPlayer(string name, int score)
        {
            var playersCollection = db.GetCollection<BsonDocument>("Players");

            var playerDocument = new BsonDocument
            {
                { "name", name },
                { "score", score }
            };

            await playersCollection.InsertOneAsync(playerDocument);
        }
        public async Task DeletePlayer(string playerName)
        {
            var playersCollection = db.GetCollection<BsonDocument>("Players");

            var filter = Builders<BsonDocument>.Filter.Eq("name", playerName);

            await playersCollection.DeleteOneAsync(filter);
        }
        public async Task EditPlayer(string playerName, int newScore)
        {
            var playersCollection = db.GetCollection<BsonDocument>("Players");

            var filter = Builders<BsonDocument>.Filter.Eq("name", playerName);
            var update = Builders<BsonDocument>.Update.Set("score", newScore);

            await playersCollection.UpdateOneAsync(filter, update);
        }
    }
}
