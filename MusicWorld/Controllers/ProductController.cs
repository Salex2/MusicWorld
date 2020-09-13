using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicData;
using MusicData.Models;
using MusicWorld.Models;
using MusicWorld.Services;

namespace MusicWorld.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        [BindProperty]
        public ProductViewModel ProductTest { get; set; }

        

        private readonly MusicContext _db;
        private readonly IProduct _product;

        public ProductController(MusicContext db,IProduct product)
        {
            _db = db;
            _product = product; 
        }

        [Route("")]
        public IActionResult Index()
        {
            var products = _product.GetProducts();

            return View(products);
        }

        [HttpGet]
        [Route("detail/{name}")]
        public IActionResult Detail(string name)
        {
           
            var product = _product.GetProduct(name);
            if (product == null)
                return RedirectToAction("Index", "Product");
            else

            return View(product);
        }

        [HttpPost]
        [Route("detail/{name}")]
        public IActionResult Detail()
        {

            var current_id = HttpContext.Session.GetString("id");
            HttpContext.Session.SetString("id", ProductTest.ProductSessionId);
            

            return RedirectToAction("Index", "Product");
        }






    }
    }
