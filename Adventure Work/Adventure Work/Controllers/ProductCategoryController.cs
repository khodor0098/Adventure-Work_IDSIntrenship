using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adventure_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        public readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        [HttpPost]
        public IActionResult AddCategory(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int categoryId = _productCategoryRepository.Add(productCategory);
            if(categoryId == 0)
            {
                return BadRequest("Error in inserting");
            }
            return Ok(categoryId);
        }
        [HttpPut]
        public IActionResult Update(ProductCategory productCategory){
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_productCategoryRepository.CategoryExists(productCategory.ProductCategoryID)== false)
            {
                return NotFound("The Product not Found");
            }
            int nbrowsAffected = _productCategoryRepository.Update(productCategory);
            if(nbrowsAffected == 0)
            {
                return BadRequest("Error in Updateing");
            }
            return Ok(nbrowsAffected);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (_productCategoryRepository.CategoryExists(id) == false)
            {
                return NotFound();
            }

            int CategoryDeleted = _productCategoryRepository.Delete(id);
            if(CategoryDeleted == 0)
            {
                return BadRequest("Error");
            }
            return Ok($"Number of products deleted: {CategoryDeleted}");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetAllProducts(int id)
        {
            if (_productCategoryRepository.CategoryExists(id) == false)
            {
                return NotFound();
            }
            List<Product> products = (List<Product>)_productCategoryRepository.GetProductsByCategory(id);
            if(products == null)
            {
                return BadRequest("Error");
            }
            return Ok(products);

        }

    }
}
