using FACTSERP.Models;

namespace FACTSERP.Repository
{
    public interface istockOuts
    {                      
        void Add(stockOuts stockOuts);
        stockOuts GestockOutsById(int id);
        void Remove(int id);
    }
}
