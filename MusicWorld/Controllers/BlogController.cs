using Microsoft.AspNetCore.Mvc;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {

        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.Posted).Take(4).ToArray(); //Retrieves  latest 4 posts from Db  

            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year,int month,string key)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Key == key);

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

            _db.Posts.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Post", "Blog", new //passing the Post parameters 
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}
