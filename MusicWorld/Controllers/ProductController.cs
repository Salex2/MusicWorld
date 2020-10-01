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
using MusicWorld.Services.Cart;

namespace MusicWorld.Controllers
{
    [Route("Product")]
    public class ProductController : Controller 
    {


        private readonly MusicContext _db;
        private readonly IProduct _product;
        [BindProperty]
        public ProductViewModel Product { get; set; }
        [BindProperty]
        public CartViewModel CartViewModel { get; set; }
       

        public ProductController(MusicContext db,IProduct product)
        {
           
            _db = db;
            _product = product;
            
        }

        //All Products
        [Route("")]
        public IActionResult Index()
        {
            var products = _product.GetProducts();

            return View(products);
        }

        //Detail Product
        [HttpGet]
        [Route("detail/{name}")]
        public IActionResult Detail(string name)
        {
           
             Product = _product.GetProduct(name);
            if (Product == null)
                return RedirectToAction("Index", "Product");
            else

            return View(Product); 
        }


        //Add to Cart 
        [HttpPost]
        [Route("detail/{name}")]
        public IActionResult Detail()
        {
            new AddToCart(HttpContext.Session).Add(CartViewModel);

            return RedirectToAction("Cart", "Product");

        }


        //Cart show detail
        [Route("Cart")]
        [HttpGet]
        public IActionResult Cart()
        {
            var Cart = new GetCart(HttpContext.Session, _db).Get();

            return View(Cart);
        }
      


    }
    }
