using Adventure_Work.Models;

namespace Adventure_Work.Interfaces
{
    public interface IProductModelRepository
    {
        bool GetById(int id);
    }
}
