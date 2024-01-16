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

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item?> GetItemAsync(Guid id)
        {
            return await Task.FromResult(items.FirstOrDefault(a=>a.Id == id));
        }

        public async Task CreateItemAsync(Item item)
        {
            await Task.Run(() => items.Add(item));
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = await Task.FromResult(items.FindIndex(a=>a.Id.Equals(item.Id)));
            items[index] = item;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = await Task.FromResult(items.FindIndex(a=>a.Id.Equals(id)));
            await Task.Run(() => items.RemoveAt(index));
        }
    }
}