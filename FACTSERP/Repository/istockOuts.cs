using FACTSERP.Models;

namespace FACTSERP.Repository
{
    public interface istockOuts
    {

        IEnumerable<stockOuts> GetAllstockOuts { get; }

        void Add(stockOuts stockOuts);

        stockOuts GestockOutsById(int id);

        void Remove(int id);
    }
}
