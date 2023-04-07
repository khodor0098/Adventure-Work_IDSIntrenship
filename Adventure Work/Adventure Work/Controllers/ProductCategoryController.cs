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
            return Ok(nbrowsAffected);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (_productCategoryRepository.CategoryExists(id) == false)
            {
                return NotFound();
            }

            int CategoryDeleted = _productCategoryRepository.Delete(id);
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
            return Ok(products);

        }

    }
}
