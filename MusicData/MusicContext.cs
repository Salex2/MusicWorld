using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicData.Models;

namespace MusicData
{
    public class MusicContext : IdentityDbContext
    {
   
        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
         
        public DbSet<Post> Posts { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Songs> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
