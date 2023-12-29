using Catalog.Models;

namespace Catalog.Repositories
{
    public interface IInMemItems
    {
        Item? GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}