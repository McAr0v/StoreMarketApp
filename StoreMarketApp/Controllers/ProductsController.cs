using Microsoft.AspNetCore.Mvc;
using StoreMarketApp.Abstractions;
using StoreMarketApp.Contexts;
using StoreMarketApp.Contracts.Requests;
using StoreMarketApp.Contracts.Responses;
using StoreMarketApp.Models;
using StoreMarketApp.Services;

namespace StoreMarketApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductsController : ControllerBase
    {
        private readonly StoreContext storeContext;
        private readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices, StoreContext context)
        {
            storeContext = context;
            _productServices = productServices;
        }

        [HttpGet][Route("products/{id}")]
        public ActionResult<ProductResponse> GetProduct(int id)
        {
            var product = _productServices.GetProductById(id);
            return Ok(product);
        }

        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<ProductResponse>> GetProducts()
        {
            var products = _productServices.GetProducts();

            return Ok(products);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<int> AddProducts(ProductCreateRequest request)
        {

            try
            {
                int id = _productServices.AddProduct(request);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet(template: "GetProductsCSV")]
        public FileContentResult GetProductsCsv()
        {
            var products = _productServices.GetProducts();
            var content = _productServices.GetCsv(products);

            return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");

        }

        /*[HttpDelete]
        [Route("products/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var productToDelete = storeContext.Products.FirstOrDefault(x => x.Id == id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            try
            {
                storeContext.Products.Remove(productToDelete);
                storeContext.SaveChanges();

                return Ok(new { Message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

    }
}
