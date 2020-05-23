using Microsoft.AspNetCore.Mvc;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Controllers
{
    [Route("blog")]
    public class Blog : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{year:min(2000)}/{month:range(1,12)}")]
        public IActionResult Post()
        {
            var post = new Post
            {
                Title = "TITLE",
                Body = "Body",
                Author = "Author",
                Posted = DateTime.Now,
            };

            return View(post);
        }

        [HttpGet,Route("create")]  //displaying the view; MVC posts the data back to this action so we make a new action to
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost,Route("create")] //handles the form Post; now the MVC post the data back from the browser to this action
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return View();

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            return View();
        }
    }
}
