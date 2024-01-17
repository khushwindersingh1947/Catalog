namespace Catalog.Api.Dtos
{
    //Data transfer Object
    //this will not expose our Item Model
    public record ItemDto
    {
        //init means we can only set value when we initialize Item record
        public Guid Id { get; init; }   
        public string? Name {get;init;}
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}