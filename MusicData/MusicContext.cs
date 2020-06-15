using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
