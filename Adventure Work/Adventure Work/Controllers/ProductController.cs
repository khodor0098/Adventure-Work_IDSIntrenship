using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using Adventure_Work.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adventure_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductModelRepository _productModelRepository;
        private readonly IProductSubCategoryRepository _productSubCategoryRepository;
        private readonly IUnitMeasureRepository _unitMeasureRepository;

        public ProductController(IProductRepository productRepository, IProductModelRepository productModelRepository,
            IProductSubCategoryRepository productSubCategoryRepository, IUnitMeasureRepository unitMeasureRepository)
        {
            _productRepository=productRepository;
            _productModelRepository=productModelRepository;
            _productSubCategoryRepository=productSubCategoryRepository;
            _unitMeasureRepository=unitMeasureRepository;
        }
        //Insert New Product
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check if the sub category exists
            bool productSubCategory = _productSubCategoryRepository.GetById(product.ProductSubcategoryID);
            if (productSubCategory == false)
            {
                return BadRequest("Invalid product subcategory ID.");
            }
            //Check if the model  exists
            bool model = _productModelRepository.GetById(product.ProductModelID);
            if (model == false)
            {
                return BadRequest("Invalid product model ID.");
            }
            // Check if the size exists           
            bool unitMeasuresize = _unitMeasureRepository.GetByUnitMeasureCode(product.SizeUnitMeasureCode);
            if(unitMeasuresize == false)
            {
                return BadRequest("Invaild SizeUnitMeasureCode");
            }
            // Check if the weight exists
            bool unitMeasureweight = _unitMeasureRepository.GetByUnitMeasureCode(product.WeightUnitMeasureCode);
            if (unitMeasureweight == false)
            {
                return BadRequest("Invaild WeightUnitMeasureCode");
            }
            int productId = _productRepository.Add(product);
            return Ok(productId);
        }
        //update product 
        [HttpPut]
        public IActionResult Update(Product product)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_productRepository.ProductExists(product.ProductID)==false)
            {
                return NotFound("The Product not Found");
            }
            //Check if the sub category exists
            bool productSubCategory = _productSubCategoryRepository.GetById(product.ProductSubcategoryID);
            if (productSubCategory == false)
            {
                return BadRequest("Invalid product subcategory ID.");
            }
            //Check if the model  exists
            bool model = _productModelRepository.GetById(product.ProductModelID);
            if (model == false)
            {
                return BadRequest("Invalid product model ID.");
            }
            // Check if the size exists           
            bool unitMeasuresize = _unitMeasureRepository.GetByUnitMeasureCode(product.SizeUnitMeasureCode);
            if (unitMeasuresize == false)
            {
                return BadRequest("Invaild SizeUnitMeasureCode");
            }
            // Check if the weight exists
            bool unitMeasureweight = _unitMeasureRepository.GetByUnitMeasureCode(product.WeightUnitMeasureCode);
            if (unitMeasureweight == false)
            {
                return BadRequest("Invaild WeightUnitMeasureCode");
            }
            int nbrowsAffected = _productRepository.UpdateProduct(product);
            return Ok(nbrowsAffected);
        }
        //Delete Product
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            bool productEx = _productRepository.ProductExists(id);
            if (productEx == false)
            {
                return NotFound();
            }

            int productsDeleted = _productRepository.DeleteProduct(id);          
            return Ok($"Number of products deleted: {productsDeleted}");
        }
        //Get ALL Products
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetAllProducts()
        {
            List<Product> products = (List<Product>)_productRepository.GetAllProducts();
            return Ok(products);
    
        }
    }
}
