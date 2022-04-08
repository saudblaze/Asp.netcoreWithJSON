using FACTSERP.Models;
using FACTSERP.Repository;
using FACTSERP.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FACTSERP.Controllers
{
    public class StockInController : Controller
    {
        private readonly istockIns istockIns;
        private readonly iproducts iproducts;
        private readonly HelperClass objHelper;

        public StockInController(istockIns _istockIns, iproducts _iproducts, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            istockIns = _istockIns;
            iproducts = _iproducts;
            objHelper = new HelperClass(hostingEnvironment);
        }
        public IActionResult Index()
        {
            ERPFacts objDB = objHelper.GetDatabase();
            List<products> objProducts = objDB.products.ToList();
            List<stockIns> listockIns = objDB.stockIns.Select(x => new stockIns
            {
                id = x.id,
                productId = x.productId,
                productName = objProducts.Where(y => y.id == x.productId).Select(x => x.name).FirstOrDefault(),
                qty = x.qty
            }).OrderByDescending(x => x.id).ToList();
            return View(listockIns);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            ERPFacts objDB = objHelper.GetDatabase();
            List<products> objProducts = objDB.products.ToList();
            ViewBag.ProductList = objProducts;
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(stockIns stockIns)
        {
            try
            {
                    //check if product is exist
                    products productsExists = iproducts.GeProductsById(stockIns.productId);
                    if (productsExists != null)
                    {
                        if (stockIns != null && stockIns.productId != 0)
                        {
                            stockIns objstockIns = new stockIns();
                            objstockIns.productId = stockIns.productId;
                            objstockIns.qty = stockIns.qty;
                            istockIns.Add(objstockIns);

                            //update product qty
                            products updateProductsQty = iproducts.GeProductsById(stockIns.productId);
                            updateProductsQty.qty = Convert.ToInt32(updateProductsQty.qty + stockIns.qty);
                            iproducts.Update(updateProductsQty);

                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.Message = "Already Exist!!";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Product does not Exist!!";
                        return View();
                    }                
            }
            catch (Exception ex)
            { 
            
            }

            return View();
        }

        
        public ActionResult Delete(int id)
        {
            return View(istockIns.GeStocksById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {                                 

                //Also update prodtuct qty
                stockIns stockIns = istockIns.GeStocksById(id);                 

                if (stockIns != null)
                {
                    // update product qty
                    products updateProductsQty = iproducts.GeProductsById(stockIns.productId);
                    updateProductsQty.qty = Convert.ToInt32(updateProductsQty.qty - stockIns.qty);
                    iproducts.Update(updateProductsQty);
                    istockIns.Remove(id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Already Exist!!";
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }
    }
}
