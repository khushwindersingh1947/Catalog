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
        private readonly IInMemItems _repository;

        public ItemsController(IInMemItems repository)
        {
            _repository = repository;
        }

        // Get /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = _repository.GetItems();
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);
            if(item is not null)
            {
                return item;
            }
            return NotFound();
        }
    }
}