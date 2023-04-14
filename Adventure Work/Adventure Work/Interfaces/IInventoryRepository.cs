using Adventure_Work.Models;

namespace Adventure_Work.Interfaces
{
    public interface IInventoryRepository
    {
        int Add(Inventory inventory);
        int Update(Inventory inventory);
        bool InventoryExist(short locationId, int productId);
        ICollection<Product> GetProductsInShelf(string shelf);
        ICollection<ProductQuantity> GetProductQuantities();
    }
}
