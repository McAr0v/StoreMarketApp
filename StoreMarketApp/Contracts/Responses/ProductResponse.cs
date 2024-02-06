using StoreMarketApp.Models;
using System.ComponentModel.DataAnnotations;

namespace StoreMarketApp.Contracts.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; } = decimal.Zero;

        /*public ProductResponse(Product product) 
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            CategoryId = product.CategoryId;
            Price = product.Price;

        }*/

    }
}
