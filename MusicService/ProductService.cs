using MusicData;
using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicService
{
    public class ProductService : IMusicProducts
    {
        private MusicContext _db;

        public ProductService(MusicContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAllProducts() =>
           _db.Products.ToList().Select(x => new Product
           {
               Name = x.Name,
               Description = x.Description,
               Value = $"$ {x.Value.ToString()}",  //1100.50 => 1,100.50 => $1,100.50
            });
    }
}
