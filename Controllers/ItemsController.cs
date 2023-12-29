using Catalog.Models;
using Catalog.Repositories;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController:ControllerBase
    {
        private readonly InMemItems repository;

        public ItemsController()
        {
            repository = new InMemItems();
        }

        // Get /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }
    }
}