using Microsoft.AspNetCore.Mvc;
using MusicData;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicWorld.Services;
using static MusicWorld.Services.ProductService;
using MusicWorld.Models.Products;

namespace MusicWorld.Controllers
{
    [Route("[Controller]")]
    public class AdminController : Controller
    {

        private readonly MusicContext _db;


        public AdminController(MusicContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {

            return View();
        }

        [Route("Stock")]
        public IActionResult Stock()
        {

            return View();
        }

        [Route("Orders")]
        public IActionResult Orders()
        {
            return View();
        }


        //PRODUCTS

        [HttpGet, Route("products")]
        public IActionResult GetProducts()
        {
            var products = new ProductService(_db).GetAllProducts();

            return Ok(products);

        }


        [HttpGet, Route("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new ProductService(_db).GetProduct(id));

        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok((await new ProductService(_db).Delete(id)));
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductViewModel request)
        {
            return Ok((await new ProductService(_db).Create(request)));
        }
        //we use FromBody because we are posting json

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductViewModel request)
        {
            return Ok((await new ProductService(_db).Update(request)));
        }




        //STOCKS

        [HttpGet, Route("stocks")]
        public IActionResult GetStocks()
        {
            return Ok(new StockService(_db).GetStock());
        }
       
        [HttpDelete("stocks/{id}")]
        public async Task<IActionResult> DeleteStock(int id) 
        { 
            return  Ok((await new StockService(_db).DeleteStock(id)));
        }

        [HttpPost("stocks")]
        public async Task<IActionResult> CreateStock([FromBody] StockViewModel request)

        {
           return Ok((await new StockService(_db).Create(request)));
            //we use FromBody because we are posting json
        }

        [HttpPut("stocks")]
        public async Task<IActionResult> UpdateStock([FromBody] StockViewModel request)
        {
           return Ok((await new StockService(_db).UpdateStock(request)));
        }
    }
        


    }

