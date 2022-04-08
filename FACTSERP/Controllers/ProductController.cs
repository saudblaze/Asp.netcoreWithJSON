using FACTSERP.Models;
using FACTSERP.Repository;
using FACTSERP.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FACTSERP.Controllers
{
    public class ProductController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        private readonly iproducts iproducts;

        public ProductController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, iproducts _iproducts)
        {
            _hostingEnvironment = hostingEnvironment;
            iproducts = _iproducts;
        }
        public IActionResult Index()
        {
            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path
            var fullPath = Path.Combine(rootPath, "JSONDatabase/ERPFacts.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            if (string.IsNullOrWhiteSpace(jsonData)) return null; //if no data is present then return null or error if you wish

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            //var jsonString = File.ReadAllText("my-model.json");
            var jsonModel = JsonSerializer.Deserialize<ERPFacts>(jsonData, options);
            var modelJson = JsonSerializer.Serialize(jsonModel, options);




            return View(iproducts.GetAllProducts);
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
        [ValidateAntiForgeryToken]
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
