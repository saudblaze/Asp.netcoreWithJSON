using FACTSERP.Models;

namespace FACTSERP.Repository
{
    public interface iproducts
    {
        IEnumerable<products> GetAllProducts { get; }
        
        products GeProductsById(int id);
        products GetproductsByName(string name);
        void Add(products products);
        void Update(products products);

        void Remove(int id);
    }

}
