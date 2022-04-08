using FACTSERP.Models;

namespace FACTSERP.Repository
{
    public interface istockIns
    {
        void Add(stockIns stockIns);
        stockIns GeStocksById(int id);
        void Remove(int id);
    }
}
