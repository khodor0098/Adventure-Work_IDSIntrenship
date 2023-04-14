using Adventure_Work.Interfaces;
using Adventure_Work.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adventure_Work.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPhotoController : ControllerBase
    {
        private IProductPhotoRepository _productPhotoRepository;
        public ProductPhotoController(IProductPhotoRepository productPhotoRepository)
        {
            _productPhotoRepository = productPhotoRepository;
        }
        [HttpGet("ThumbNailPhoto/{id}")]
        public IActionResult GetThumbnailByProductId(int id)
        {
            byte[] ThumbNailPhoto = _productPhotoRepository.GetThumbnailByProductId(id);
            if (ThumbNailPhoto == null)
            {
                return NotFound("Error");
            }
            return Ok(ThumbNailPhoto);

        }
        [HttpGet("LargePhoto/{id}")]
        public IActionResult GetLargePhotoByProductId(int id)
        {
            byte[] LargePhoto = _productPhotoRepository.GetLargePhotoByProductId(id);
            if (LargePhoto == null)
            {
                return NotFound("Error");
            }
            return Ok(LargePhoto);

        }
        [HttpGet]
        public IActionResult GetallPhoto()
        {
            List<ProductPhoto> productPhotos = (List<ProductPhoto>)_productPhotoRepository.GetAllProductPhotos();
            if (productPhotos == null)
            {
                return NotFound("Error");
            }
            return Ok(productPhotos);

        }
    }
}
