using Microsoft.AspNetCore.Mvc;
using MusicData;

using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicWorld.Services;
using static MusicWorld.Services.ProductService;

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

        [HttpGet, Route("products")]
        public IActionResult GetProducts() => Ok(new ProductService(_db).GetAllProducts());

        [HttpGet, Route("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new ProductService(_db).GetProduct(id));

        [HttpDelete("products/{id}")]
        public async Task<IActionResult>  DeleteProduct(int id) => Ok((await new ProductService(_db).Delete(id)));

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductService.Request request) => Ok((await new ProductService(_db).Create(request)));
        //we use FromBody because we are posting json

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductService.Request request) => Ok((await new ProductService(_db).Update(request)));
         

        }
        


    }

