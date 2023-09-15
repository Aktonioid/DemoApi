using DemoApi.Settings;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading;

namespace DemoApi.Repositories.DB
{
    public class Mongo : IDBfunctions
    {
        private readonly MongoDBClient client = new();


        private string table;

        public Mongo(string table)
        {
            this.table = table;
        }

        //private void setter(string table)
        //{
        //    Table = table;
        //}

        public async Task CreateAsync<Object>(Object obj)
        {
            var collection = client.db.GetCollection<Object>(table);
            await collection.InsertOneAsync(obj);
            
        }

        public async Task DeleteAsync<Object>(Guid id)
        {
            var collection = client.db.GetCollection<Object>(table);
            var filter = Builders<Object>.Filter.Eq("_id", id);
            await collection.DeleteOneAsync(filter);
        
        }

        public async Task<List<Object>> FindAsync<Object>()
        {
            var collection = client.db.GetCollection<Object>(table);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Object> FindByIdAsync<Object>(Guid id)
        {
            var collection = client.db.GetCollection<Object>(table);
            var filter = Builders<Object>.Filter.Eq("_id", id);

            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Object> FindByLoginAsync<Object>(string login)
        {
            var collection = client.db.GetCollection<Object>(table);
            var filter = Builders<Object>.Filter.Eq("login", login);

            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Object> FindByKeyAsync<Object>(int key)
        {
            var collection = client.db.GetCollection<Object>(table);
            var filter = Builders<Object>.Filter.Eq("key", key);

            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync<Object>(Object obj, Guid id)
        {
            var collection = client.db.GetCollection<Object>(table);
            var filter = Builders<Object>.Filter.Eq("_id",id);

            await collection.ReplaceOneAsync(filter, obj);
        }

        public async Task UpdateByKeyAsync<Object>(Object obj, int key)
        {
            var collection = client.db.GetCollection<Object>(table);
            var filter = Builders<Object>.Filter.Eq("key", key);

            await collection.ReplaceOneAsync(filter, obj);
        }

        public async Task DeleteByKeyAsync<Object>(int key)
        {
            var collection = client.db.GetCollection<Object>(table);
            var filter = Builders<Object>.Filter.Eq("key", key);
            await collection.DeleteOneAsync(filter);
        }

        
    }
}
