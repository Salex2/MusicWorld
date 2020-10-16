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

        public async Task<ProductViewModel> GetProduct(string name)
        {

           var stocksOnHold = _db.StocksOnHold.Where(x => x.ExpireDate < DateTime.Now).ToList();


            //here we remove the stock or put it back into our actual Stock db
            if(stocksOnHold.Count > 0)
            {
                var stockToReturn = _db.Stock.Where(x => stocksOnHold.Any(y => y.StockId == x.Id)).ToList();


                //restore the quantity
                foreach(var stock in stockToReturn)
                {
                    stock.Quantity = stock.Quantity + stocksOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }

                _db.StocksOnHold.RemoveRange(stocksOnHold);

                await _db.SaveChangesAsync();
            }


           return _db.Products
                .Include(x => x.Stock)
                .Where(x => x.Name == name)
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Value =  $"$ {x.Value.ToString("N2")}",

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
                Value = $"$ {x.Value.ToString("N2")}",
            });
        }
    }
}
