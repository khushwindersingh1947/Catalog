using Catalog.Models;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalog";
        private readonly string collectionName = "items";
        private readonly IMongoCollection<Item> _itemsCollection;
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
            _itemsCollection.DeleteOne(a=>a.Id == id);
        }

        public Item? GetItem(Guid id)
        {
            return _itemsCollection.Find(id);
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}