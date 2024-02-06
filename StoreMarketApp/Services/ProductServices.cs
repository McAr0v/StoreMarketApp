using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using StoreMarketApp.Abstractions;
using StoreMarketApp.Contexts;
using StoreMarketApp.Contracts.Requests;
using StoreMarketApp.Contracts.Responses;
using StoreMarketApp.Models;
using System.Text;

namespace StoreMarketApp.Services
{
    public class ProductServices : IProductServices
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ProductServices(StoreContext storeContext, IMapper mapper, IMemoryCache memoryCache) 
        {
            _storeContext = storeContext;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public int AddProduct(ProductCreateRequest product)
        {
            var entity = _mapper.Map<Product>(product);
            _storeContext.Products.Add(entity);
            _storeContext.SaveChanges();

            _memoryCache.Remove("products");

            return entity.Id;

        }

        public string GetCsv(IEnumerable<ProductResponse> products)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var product in products)
            {
                stringBuilder.AppendLine(product.Id + ";" + product.Name + ";" + product.Description + ";" + product.Price + "\n");
            }

            return stringBuilder.ToString();
        }

        public ProductResponse? GetProductById(int productId)
        {
            var product = _storeContext.Products.FirstOrDefault(x=> x.Id == productId);

            if (product == null)
            {
                return null;
            }
            
            return _mapper.Map<ProductResponse>(product);

        }

        public IEnumerable<ProductResponse> GetProducts()
        {
            if (_memoryCache.TryGetValue("products", out IEnumerable<ProductResponse> prods))
            {
                return prods;
            }

            IEnumerable<ProductResponse> products = _storeContext.Products.Select(x=> _mapper.Map<ProductResponse>(x)).ToList();

            _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));

            return products;
        }
    }
}
