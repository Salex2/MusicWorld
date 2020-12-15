using MusicWorld.Models;
using MusicWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services.Blog
{
   public interface IBlog
    {
        Task<bool> Delete(int Id);
        PostViewModel Edit(PostViewModel post);
    }
}
