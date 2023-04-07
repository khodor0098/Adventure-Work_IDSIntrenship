using Adventure_Work.Models;

namespace Adventure_Work.Interfaces
{
    public interface IProductCategoryRepository
    {
        int Add(ProductCategory productCategory);
        int Update(ProductCategory productCategory);
        bool CategoryExists(int productCategoryId);
        int Delete(int productCategoryId);
        ICollection<Product> GetProductsByCategory(int productCategoryId);
    }
}
