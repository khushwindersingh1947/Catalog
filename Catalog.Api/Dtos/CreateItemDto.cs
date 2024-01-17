using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record CreateItemDto
    {
        [Required]
        public string? Name {get;init;}
        
        [Required]
        [Range(minimum:1,maximum:1000)]
        public decimal Price { get; init; }
    }
}