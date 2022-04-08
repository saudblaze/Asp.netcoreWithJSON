using FACTSERP.Models;
using FACTSERP.Repository;
using FACTSERP.Utilities;
using FACTSERP.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FACTSERP.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly iproducts iproducts;
        private readonly HelperClass objHelper;

        public ProductController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, iproducts _iproducts)
        {
            iproducts = _iproducts;
            objHelper = new HelperClass(hostingEnvironment);
        }
        public IActionResult Index()
        {
            ERPFacts objDB = objHelper.GetDatabase();
            List<products> liproducts = objDB.products.OrderByDescending(x => x.id).ToList();
            return View(liproducts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(products products)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    products productsExist = iproducts.GetproductsByName(products.name);
                    if (productsExist == null)
                    {
                        products objproducts = new products();
                        objproducts.id = products.id;
                        objproducts.name = products.name;
                        objproducts.qty = products.qty;
                        iproducts.Add(objproducts);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Already Exist!!";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(iproducts.GeProductsById(id));
        }

        [HttpPost]
        public ActionResult Update(products products)
        {
            try
            {
                products objproducts = iproducts.GeProductsById(products.id);
                objproducts.name = products.name;
                objproducts.qty = products.qty;

                iproducts.Update(objproducts);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(iproducts.GeProductsById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                iproducts.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
