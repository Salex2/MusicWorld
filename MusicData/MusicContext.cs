using Microsoft.EntityFrameworkCore;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicData
{
    public class MusicContext : DbContext
    {
   
        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
         
        public DbSet<Post> Posts { get; set; }
    }
}
