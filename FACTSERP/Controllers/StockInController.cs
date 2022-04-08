using FACTSERP.Models;
using FACTSERP.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FACTSERP.Controllers
{
    public class StockInController : Controller
    {
        private readonly istockIns istockIns;
        private readonly iproducts iproducts;

        public StockInController(istockIns _istockIns, iproducts _iproducts)
        {
            istockIns = _istockIns;
            iproducts = _iproducts;
        }
        public IActionResult Index()
        {
            return View(istockIns.GetAllStockIn);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ProductList = iproducts.GetAllProducts;
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

        #region not required
        public IActionResult Edit(int id)
        {
            ViewBag.ProductList = iproducts.GetAllProducts;
            return View(istockIns.GeStocksById(id));
        }

        public ActionResult Update(stockIns stockIns)
        {
            try
            {

                stockIns objstockIns = istockIns.GeStocksById(stockIns.id);
                objstockIns.productId = stockIns.productId;
                objstockIns.qty = stockIns.qty;


                //istockIns.Update(objstockIns);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        #endregion
        public ActionResult Delete(int id)
        {
            return View(istockIns.GeStocksById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                

                //Also update prodtuct qty
                stockIns stockIns = istockIns.GeStocksById(id);

                istockIns.Remove(id);

                if (stockIns != null)
                {
                    // update product qty
                    products updateProductsQty = iproducts.GeProductsById(stockIns.productId);
                    updateProductsQty.qty = Convert.ToInt32(updateProductsQty.qty - stockIns.qty);
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
