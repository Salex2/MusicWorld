using Microsoft.EntityFrameworkCore;
using MusicData;
using MusicData.Models;
using MusicWorld.Models;
using MusicWorld.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services
{
    public class UserProductService : IProduct
    {

        private readonly MusicContext _db;

        public UserProductService(MusicContext db)
        {
            _db = db;
        }

        public ProductViewModel GetProduct(string name)
        {
           return _db.Products
            .Include(x => x.Stock)
            .Where(x => x.Name == name)
            .Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = $"$ {x.Value.ToString("N2")}",

                Stock = x.Stock.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    InStock = y.Quantity > 0

                })
            })
            .FirstOrDefault();
        }  
                    
            
        


        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _db.Products.ToList().Select(x => new ProductViewModel
            {
                Description = x.Description,
                Name = x.Name,
                Value = $"$ {x.Value.ToString("N2")}"
            });
        }
    }
}
