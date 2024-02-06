using StoreMarketApp.Contracts.Requests;
using StoreMarketApp.Contracts.Responses;

namespace StoreMarketApp.Abstractions
{
    public interface IProductServices
    {
        public int AddProduct(ProductCreateRequest product);

        public IEnumerable<ProductResponse> GetProducts();

        public ProductResponse GetProductById(int productId);

        public string GetCsv(IEnumerable<ProductResponse> products);
    }
}
