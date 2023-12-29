namespace Catalog.Models
{
    //immutable, can use == operator and also "with" expression
    public record Item
    {
        //init means we can only set value when we initialize Item record
        public Guid Id { get; init; }   
        public string? Name {get;init;}
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}