using Adventure_Work.Models;

namespace Adventure_Work.Interfaces
{
    public interface IProductPhotoRepository
    {
        byte[] GetThumbnailByProductId(int productId);
        byte[] GetLargePhotoByProductId(int productId);
        ICollection<ProductPhoto> GetAllProductPhotos();
    }
}
