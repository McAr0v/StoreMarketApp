using Microsoft.AspNetCore.Mvc;
using StoreMarketApp.Contexts;
using StoreMarketApp.Contracts.Requests;
using StoreMarketApp.Contracts.Responses;
using StoreMarketApp.Models;

namespace StoreMarketApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly StoreContext storeContext;
        public CategoryController(StoreContext context)
        {
            storeContext = context;
        }

        [HttpGet]
        [Route("categories/{id}")]
        public ActionResult<CategoryResponse> GetCategory(int id)
        {
            var result = storeContext.Categories.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new CategoryResponse(result));
            }
        }

        [HttpGet]
        [Route("categories")]
        public ActionResult<IEnumerable<CategoryResponse>> GetCategories()
        {
            var result = storeContext.Categories;

            return Ok(result.Select(x => new CategoryResponse(x)));
        }

        [HttpPost]
        [Route("categories")]
        public ActionResult<CategoryResponse> AddCategory(CategoryCreateRequest request)
        {
            Category category = request.GetEntity();


            try
            {

                var result = storeContext.Categories.Add(category).Entity;
                storeContext.SaveChanges();
                return Ok(new CategoryResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("categories/{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var categoryToDelete = storeContext.Categories.FirstOrDefault(x => x.Id == id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            try
            {
                storeContext.Categories.Remove(categoryToDelete);
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
