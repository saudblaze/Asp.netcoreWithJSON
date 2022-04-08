using FACTSERP.Models;
using FACTSERP.Utilities;

namespace FACTSERP.Repository
{
    public class productsRepository : iproducts
    {                                    
        private readonly HelperClass objHelper;
        public productsRepository(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            objHelper = new HelperClass(hostingEnvironment);
        }
        

        public products GeProductsById(int id)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            return objDB.products.Where(x => x.id == id).FirstOrDefault();
        }

        public products GetproductsByName(string name)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            return objDB.products.Where(x => x.name == name).FirstOrDefault();
        }

        public void Add(products products)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            products.id = objDB.products.Count + 1;
            objDB.products.Add(products);

            objHelper.SaveChanges(objDB);
        }

        public void Update(products products)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            List<products> productsList = objDB.products.ToList();
            foreach (products item in productsList)
            {
                if (item.id == products.id)
                {
                    item.name = products.name;
                    item.qty = products.qty;
                }
            }
            objDB.products = productsList;
            objHelper.SaveChanges(objDB);
        }

        public void Remove(int id)
        {
            ERPFacts objDB = objHelper.GetDatabase();
            objDB.products.RemoveAll(x => x.id == id);
            objHelper.SaveChanges(objDB);
        }
    }
}
