using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicData;
using MusicWorld.Models;

namespace MusicWorld.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {

        private readonly MusicContext _db;

        public ProductController(MusicContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            var posts = _db.Products.OrderByDescending(x => x.Id).Take(4).ToArray(); //Retrieves  latest 4 posts from Db  

            return View(posts);
        }

        [HttpPost, Route("create")]
        public IActionResult Create(int id, string Name, string Description)
        {
            if (!ModelState.IsValid)
                return View();

            _db.Products.Add(new Product
            {
                Id = id,
                Name = Name,
                Description = Description
            });

            return View();
        }
    }
}