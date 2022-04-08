using FACTSERP.Models;

namespace FACTSERP.Repository
{
    public class productsRepository  : iproducts
    {
        private DatabaseContext db;

        public productsRepository(DatabaseContext _db)
        {
            db = _db;
        }

        public IEnumerable<products> GetAllProducts => from u in db.products                                                         
                                                        select new products
                                                        {
                                                            id = u.id,
                                                            name = u.name,
                                                            qty = u.qty                                                            
                                                        };
        
        public products GeProductsById(int id)
        {
            return db.products.Where(x => x.id == id).FirstOrDefault();
        }

        public products GetproductsByName(string name)
        {
            
            return db.products.Where(x => x.name == name).FirstOrDefault();
        }

        public void Add(products products)
        {
            db.products.Add(products);
            db.SaveChanges();
        }

        public void Update(products products)
        {
            db.products.Update(products);
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            products products = db.products.Find(id);
            db.products.Remove(products);
            db.SaveChanges();
        }
    }
}
