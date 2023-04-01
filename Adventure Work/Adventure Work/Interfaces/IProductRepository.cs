using Adventure_Work.Models;

namespace Adventure_Work.Interfaces
{
    public interface IProductRepository
    {
        int Add(Product product);
        int UpdateProduct(Product product);
        int DeleteProduct(int id);
        ICollection<Product> GetAllProducts();
        bool ProductExists(int productId);
    }
}
