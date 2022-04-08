using FACTSERP.Models;

namespace FACTSERP.Repository
{
    public class stockOutsRepository : istockOuts
    {
        private DatabaseContext db;

        public stockOutsRepository(DatabaseContext _db)
        {
            db = _db;
        }

        public IEnumerable<stockOuts> GetAllstockOuts => from s in db.stockOuts
                                                      join p in db.products on s.productId equals p.id
                                                      select new stockOuts
                                                      {
                                                          id = s.id,
                                                          productId = s.productId,
                                                          qty = s.qty,
                                                          productName = p.name
                                                      };

        public void Add(stockOuts stockOuts)
        {
            db.stockOuts.Add(stockOuts);
            db.SaveChanges();
        }

        public stockOuts GestockOutsById(int id)
        {
            return (from s in db.stockOuts
                    join p in db.products on s.productId equals p.id
                    where s.id == id
                    select new stockOuts
                    {
                        id = s.id,
                        productId = s.productId,
                        qty = s.qty,
                        productName = p.name
                    }).FirstOrDefault();

        }

        public void Remove(int id)
        {
            stockOuts stockOuts = db.stockOuts.Find(id);
            db.stockOuts.Remove(stockOuts);
            db.SaveChanges();
        }
    }
}
