using FACTSERP.Models;
using FACTSERP.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FACTSERP.Controllers
{
    public class StockOutController : Controller
    {
        private readonly istockOuts istockOuts;
        private readonly iproducts iproducts;

        public StockOutController(istockOuts _istockOuts, iproducts _iproducts)
        {
            istockOuts = _istockOuts;
            iproducts = _iproducts;
        }
        public IActionResult Index()
        {
            return View(istockOuts.GetAllstockOuts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ProductList = iproducts.GetAllProducts;
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                //Also update prodtuct qty
                stockOuts stockIns = istockOuts.GestockOutsById(id);

                istockOuts.Remove(id);

                if (stockIns != null)
                {
                    // update product qty
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
            catch
            {
                return View();
            }
        }
    }
}
