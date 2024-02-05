using StoreMarketApp.Models;

namespace StoreMarketApp.Contracts.Requests
{
    public class CategoryUpdateRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }


        public Product GetEntity()
        {
            return new Product {Id = Id, Name = Name, Description = Description };
        }
    }
}
