using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using MusicData;
using MusicWorld.Models;
using MusicWorld.ViewModels;

namespace MusicWorld.Services.Blog
{
    public class BlogService : IBlog
    {

        private readonly MusicContext _db;

        public BlogService(MusicContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(int Id)
        {
          var post =  _db.Posts.FirstOrDefault(x => x.Id == Id);

            
                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();

            return true;
       
        }

        public PostViewModel Edit(PostViewModel request)
        {
            
            var post = _db.Posts.FirstOrDefault(x => x.Id == request.Id);


            post.Body = request.Body;
            post.Title = request.Title;
            post.Posted = DateTime.Now;

                _db.SaveChanges();


            return new PostViewModel
            {
                Id = post.Id,
                Body = post.Body,
                Title = post.Title,
                Posted =post.Posted
               
            };

               
            
        }
    }
}
