using FACTSERP.Models;
using FACTSERP.Repository;
using FACTSERP.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FACTSERP.Controllers
{
    public class StockOutController : Controller
    {
        private readonly istockOuts istockOuts;
        private readonly iproducts iproducts;
        private readonly HelperClass objHelper;

        public StockOutController(istockOuts _istockOuts, iproducts _iproducts, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            istockOuts = _istockOuts;
            iproducts = _iproducts;
            objHelper = new HelperClass(hostingEnvironment);
        }
        public IActionResult Index()
        {
            ERPFacts objDB = objHelper.GetDatabase();
            List<products> objProducts = objDB.products.ToList();
            List<stockOuts> listockOuts = objDB.stockOuts.Select(x => new stockOuts { 
                                                                               id = x.id,
                                                                               productId = x.productId, 
                                                                               productName = objProducts.Where(y => y.id == x.productId).Select(x => x.name).FirstOrDefault(),
                                                                               qty = x.qty
            }).OrderByDescending(x => x.id).ToList();
            return View(listockOuts);
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
        public IActionResult Create(stockOuts stockOuts)
        {
            try
            {
                    //check if product is exist
                    products productsExists = iproducts.GeProductsById(stockOuts.productId);
                    if (productsExists != null)
                    {
                        if (stockOuts != null && stockOuts.productId != 0)
                        {
                            stockOuts objstockOuts = new stockOuts();
                            objstockOuts.productId = stockOuts.productId;
                            objstockOuts.qty = stockOuts.qty;
                            istockOuts.Add(objstockOuts);

                            //update product qty
                            products updateProductsQty = iproducts.GeProductsById(stockOuts.productId);
                            updateProductsQty.qty = Convert.ToInt32(updateProductsQty.qty - stockOuts.qty);
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
            return View(istockOuts.GestockOutsById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                //Also update prodtuct qty
                stockOuts stockIns = istockOuts.GestockOutsById(id);                

                if (stockIns != null)
                {
                    // update product qty
                    products updateProductsQty = iproducts.GeProductsById(stockIns.productId);
                    updateProductsQty.qty = Convert.ToInt32(updateProductsQty.qty + stockIns.qty);
                    iproducts.Update(updateProductsQty);
                    istockOuts.Remove(id);
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
