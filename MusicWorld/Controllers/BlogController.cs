using Microsoft.AspNetCore.Mvc;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicData;
using MusicWorld.Services.Blog;
using MusicWorld.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace MusicWorld.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {

        private readonly MusicContext _db;
        private readonly IBlog _blog;

        public BlogController(MusicContext db, IBlog blog)
        {
            _db = db;
            _blog = blog;
        }

        [Route("")]
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.Posted).Take(4).ToArray(); //Retrieves  latest 4 posts from Db  

            return View(posts);
        }

        [Route("{Id}")]
        public IActionResult Post(int id)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Id == id);

            return View(post);
        }

        [HttpGet,Route("create")]  //displaying the view; MVC posts the data back to this action so we make a new action to
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost,Route("create")] //handles the form Post; now the MVC post the data back from the browser to this action
        [Authorize]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return View();
            
            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            _db.Posts.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Post", "Blog" , new //passing the Post parameters 
            {
                post.Id
                
            });
        }

        [HttpGet]
        [Route("Edit")]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var userName = User.Identity.Name;
            
            var post = _db.Posts.FirstOrDefault(x => x.Id == id);
            if (userName != post.Author)
            {
                return RedirectToAction("Error", "Blog");
 
            }
            else
                return View(new PostViewModel
                {
                    Author = User.Identity.Name,
                    Id = post.Id,
                    Body = post.Body,
                    Title = post.Title,
                    Posted = DateTime.Now
                });





        }

        [HttpPost]
        [Route("Edit")]
        [Authorize]
        public IActionResult Edit(PostViewModel post)
        {
            if (!ModelState.IsValid)
                return View();

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            var postUpdate = _blog.Edit(post);
           

            return RedirectToAction("Post", "Blog",  new //passing the Post parameters 
            {
                post.Id
            });
            
           
        }
        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
        

        [HttpPost]
        [Route("Delete")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userName = User.Identity.Name;
            var post = _db.Posts.FirstOrDefault(x => x.Id == id);

            if (userName != post.Author )
            { 
                return RedirectToAction("Error", "Blog");  
            }
            else

            _blog.Delete(id);

            return RedirectToAction("Index", "Blog");


        }
    }
}
