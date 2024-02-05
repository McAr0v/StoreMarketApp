using StoreMarketApp.Models;
using System.ComponentModel.DataAnnotations;

namespace StoreMarketApp.Contracts.Requests
{
    public class CategoryCreateRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        

        public Category GetEntity()
        {
            return new Category { Name = Name, Description = Description };
        }
    }
}
