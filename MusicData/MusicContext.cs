using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicData.Models;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)//ModelBuilder defines the shape of your entities, the relationships between them, and how they map to the database.
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderProduct>()  //here i configure a  Composit key : a primary key that consists of 2 ore more primary keys
                .HasKey(x => new { x.ProductId, x.OrderId });
            
            
        }
    }
}
