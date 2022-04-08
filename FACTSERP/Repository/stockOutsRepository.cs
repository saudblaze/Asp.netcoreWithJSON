using FACTSERP.Models;
using FACTSERP.Utilities;

namespace FACTSERP.Repository
{
    public class stockOutsRepository : istockOuts
    {
        private readonly HelperClass objHelper;
        public stockOutsRepository(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            objHelper = new HelperClass(hostingEnvironment);
        }

        
        public void Add(stockOuts stockOuts)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            stockOuts.id = objDB.stockOuts.Count + 1;
            objDB.stockOuts.Add(stockOuts);

            objHelper.SaveChanges(objDB);            
        }

        public stockOuts GestockOutsById(int id)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            List<products> objProducts = objDB.products.ToList();
            stockOuts listockOuts = objDB.stockOuts.Where(x => x.id == id).Select(x => new stockOuts
            {
                id = x.id,
                productName = objProducts.Where(y => y.id == x.productId).Select(x => x.name).FirstOrDefault(),
                qty = x.qty ,
                productId = x.productId
            }).OrderByDescending(x => x.id).FirstOrDefault();

            return listockOuts;

        }

        public void Remove(int id)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            objDB.stockOuts.RemoveAll(x => x.id == id);
            objHelper.SaveChanges(objDB);
        }
    }
}
