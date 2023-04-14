using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adventure_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILocationRepository _locationRepository;

        public InventoryController(IInventoryRepository inventoryRepository, IProductRepository productRepository, ILocationRepository locationRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
            _locationRepository = locationRepository;
        }
        [HttpPost]
        public IActionResult AddInventoryRecord(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_productRepository.ProductExists(inventory.ProductID) == false)
            {
                return BadRequest("Product id Not found");
            }
            if (_locationRepository.LocationExists(inventory.LocationID) == false)
            {
                return BadRequest("Location id Not found");
            }

            int result = _inventoryRepository.Add(inventory);
            if (result == 0)
            {
                return BadRequest("Error in inserting");
            }
            return Ok(result);
        }
        [HttpPut]
        public IActionResult Update(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_inventoryRepository.InventoryExist(inventory.LocationID, inventory.ProductID) == false)
            {
                return BadRequest("Inventory Not found");
            }
            /*if (_productRepository.ProductExists(inventory.ProductID) == false)
            {
                return BadRequest("Product id Not found");
            }
            if (_locationRepository.LocationExists(inventory.LocationID) == false)
            {
                return BadRequest("Location id Not found");
            }*/
            int nbrowsAffected = _inventoryRepository.Update(inventory);
            if (nbrowsAffected == 0)
            {
                return BadRequest("Error in Updateing");
            }
            return Ok(nbrowsAffected);
        }

        [HttpGet("{shelf}")]
        public IActionResult GetAllProducts(string shelf)
        {
            List<Product> products = (List<Product>)_inventoryRepository.GetProductsInShelf(shelf);
            if (products == null)
            {
                return BadRequest("Error");
            }
            return Ok(products);

        }
        [HttpGet]
        public IActionResult GetProductQuantities()
        {
            List<ProductQuantity> productQuantities = (List<ProductQuantity>)_inventoryRepository.GetProductQuantities();
            if (productQuantities == null)
            {
                return BadRequest("Error");
            }
            return Ok(productQuantities);

        }
    }
}