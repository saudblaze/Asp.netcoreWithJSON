using FACTSERP.Models;
using FACTSERP.Utilities;

namespace FACTSERP.Repository
{
    public class stockInsRepository    : istockIns
    {
        private readonly HelperClass objHelper;

        public stockInsRepository(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            objHelper = new HelperClass(hostingEnvironment);
        }

        public void Add(stockIns stockIns)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            stockIns.id = objDB.stockIns.Count + 1;
            objDB.stockIns.Add(stockIns);

            objHelper.SaveChanges(objDB);
        }

        public stockIns GeStocksById(int id)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            List<products> objProducts = objDB.products.ToList();
            stockIns listockIns = objDB.stockIns.Where(x => x.id == id).Select(x => new stockIns
            {
                id = x.id,
                productName = objProducts.Where(y => y.id == x.productId).Select(x => x.name).FirstOrDefault(),
                qty = x.qty,
                productId = x.productId
            }).OrderByDescending(x => x.id).FirstOrDefault();

            return listockIns;
        }

        public void Remove(int id)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            objDB.stockIns.RemoveAll(x => x.id == id);
            objHelper.SaveChanges(objDB);            
        }
    }
}
