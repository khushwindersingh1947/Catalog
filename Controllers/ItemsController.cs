using Catalog.Dtos;
using Catalog.Models;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        // Get /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = _repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);
            if (item is not null)
            {
                return item.AsDto();
            }
            return NotFound();
        }

        // convention is to return the new item created
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        //convention is to not return anything
        [HttpPut("{id}")]
        public IActionResult UpdateItem(Guid id, UpdateItemDto updateItem)
        {

            Item? item = _repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            //we can use with operator for records
            Item updated = item with 
            {
                Name = updateItem.Name,
                Price = updateItem.Price
            };

            _repository.UpdateItem(updated);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(Guid id)
        {
            Item? item = _repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            _repository.DeleteItem(id);

            return NoContent();
        }

    }
}