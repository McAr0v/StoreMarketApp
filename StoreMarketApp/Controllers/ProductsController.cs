using Microsoft.AspNetCore.Mvc;
using StoreMarketApp.Contexts;
using StoreMarketApp.Contracts.Requests;
using StoreMarketApp.Contracts.Responses;
using StoreMarketApp.Models;

namespace StoreMarketApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductsController : ControllerBase
    {
        private readonly StoreContext storeContext;
        public ProductsController(StoreContext context)
        {
            storeContext = context;
        }

        [HttpGet][Route("products/{id}")]
        public ActionResult<ProductResponse> GetProduct(int id)
        {
            var result = storeContext.Products.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new ProductResponse(result));
            }
        }

        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<ProductResponse>> GetProducts()
        {
            var result = storeContext.Products;

            return Ok(result.Select(x => new ProductResponse(x)));
        }

        [HttpPost]
        [Route("products")]
        public ActionResult<ProductResponse> AddProducts(ProductCreateRequest request)
        {
            Product product = request.GetEntity();
            

            try 
            {

                var result = storeContext.Products.Add(product).Entity;
                storeContext.SaveChanges();
                return Ok(new ProductResponse(result));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
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
        }

    }
}
