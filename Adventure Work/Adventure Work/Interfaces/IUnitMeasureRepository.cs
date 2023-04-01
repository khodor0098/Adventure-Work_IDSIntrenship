using Adventure_Work.Models;

namespace Adventure_Work.Interfaces
{
    public interface IUnitMeasureRepository
    {
        bool GetByUnitMeasureCode(string code);
    }
}
