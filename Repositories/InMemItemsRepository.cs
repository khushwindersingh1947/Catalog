using Catalog.Models;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new List<Item>()
        {
            new Item {Id = Guid.NewGuid(), CreatedDate = DateTimeOffset.Now, Name = "Potion", Price = 9.9m},
            new Item {Id = Guid.NewGuid(), CreatedDate = DateTimeOffset.UtcNow, Name = "Iron Sword", Price = 20m},
            new Item {Id = Guid.NewGuid(), CreatedDate = DateTimeOffset.Now, Name = "Bronze Shield", Price = 18m},
            new Item {Id = Guid.NewGuid(), CreatedDate = DateTimeOffset.Now, Name = "Red velvet", Price = 9.9m}
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item? GetItem(Guid id)
        {
            return items.FirstOrDefault(a=>a.Id == id);
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }
    }
}