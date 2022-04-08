using FACTSERP.Models;

namespace FACTSERP.Repository
{
    public class stockInsRepository    : istockIns
    {
        private DatabaseContext db;

        public stockInsRepository(DatabaseContext _db)
        {
            db = _db;
        }

        public IEnumerable<stockIns> GetAllStockIn => from s in db.stockIns
                                                      join p in db.products on s.productId equals p.id
                                                      select new stockIns
                                                       {
                                                            id = s.id,
                                                            productId = s.productId,
                                                            qty = s.qty,
                                                            productName = p.name
                                                       };

        public void Add(stockIns stockIns)
        {
            db.stockIns.Add(stockIns);
            db.SaveChanges();
        }

        public stockIns GeStocksById(int id)
        {
            return (from s in db.stockIns
            join p in db.products on s.productId equals p.id
            where s.id == id
            select new stockIns
            {
                id = s.id,
                productId = s.productId,
                qty = s.qty,
                productName = p.name
            }).FirstOrDefault();

        }

        public void Remove(int id)
        {
            stockIns stockIns = db.stockIns.Find(id);
            db.stockIns.Remove(stockIns);
            db.SaveChanges();
        }


    }
}
