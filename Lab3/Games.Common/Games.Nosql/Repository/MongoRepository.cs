using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Games.Nosql.Models;

namespace Games.Nosql.Repository
{
    public class MongoRepository : IRepository<GameDocument>
    {
        private readonly IMongoCollection<GameDocument> _collection;

        public MongoRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<GameDocument>("Games");
        }

        public async Task<IEnumerable<GameDocument>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<GameDocument> GetByIdAsync(string id)
        {
            return await _collection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(GameDocument entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(GameDocument entity)
        {
            await _collection.ReplaceOneAsync(g => g.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(g => g.Id == id);
        }
    }
}
