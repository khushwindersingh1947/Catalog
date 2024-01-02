using Catalog.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalog";
        private readonly string collectionName = "items";
        private readonly IMongoCollection<Item> _itemsCollection;
        private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;
        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(databaseName);
            _itemsCollection = mongoDatabase.GetCollection<Item>(collectionName);
        }
        public void CreateItem(Item item)
        {
            _itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = _filterBuilder.Eq(item => item.Id, id);
            _itemsCollection.DeleteOne(filter);
        }

        public Item? GetItem(Guid id)
        {
            var filter = _filterBuilder.Eq(item => item.Id, id);
            return _itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = _filterBuilder.Eq(item => item.Id, item.Id);
            _itemsCollection.ReplaceOne(filter,item);
        }
    }
}