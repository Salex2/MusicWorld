using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicData;
using MusicData.Models;
using MusicWorld.Models;
using MusicWorld.Services;
using MusicWorld.Services.Cart;
using MusicWorld.ViewModels;
using Stripe;

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
        [BindProperty]
        public CustomerViewModel CustomerInfo { get; set; }

     



        public ProductController(MusicContext db,IProduct product,IConfiguration config)
        {
             string PublicKey = config["Stripe:PublicKey"].ToString();
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
        public async Task<IActionResult> Detail(string name)
        {
           
             Product = await _product.GetProduct(name);
            if (Product == null)
                return RedirectToAction("Index", "Product");
            else

            return View(Product); 
        }


        //Add to Cart 
        [HttpPost]
        [Route("detail/{name}")]
        public async Task<IActionResult> Detail()
        {
            var stockAdded = await new AddToCart(HttpContext.Session,_db).Add(CartViewModel);

            if (stockAdded)
                return RedirectToAction("Cart", "Product");
            else
                //TODO add a warning
                return View();

            


        }


        //Cart show detail
        [Route("Cart")]
        [HttpGet]
        public IActionResult Cart()
        {
            var Cartt = new GetCart(HttpContext.Session, _db).Get();

            return View(Cartt);
        }



        //here we get customer information
        [HttpGet] 
        [Route("CustomerInformation")]
        public IActionResult CustomerInformation()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Get();

            if (information == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Payment", "Order");
            }
            
        }
        //Add Customer Information
        [HttpPost]
        [Route("CustomerInformation")]
        public IActionResult CustomerInformation(CustomerViewModel CustomerInfo)
        {
           

            new AddCustomerInformation(HttpContext.Session).Add(CustomerInfo);

            return RedirectToAction("Payment", "Order");
        }
        


    }
    }
