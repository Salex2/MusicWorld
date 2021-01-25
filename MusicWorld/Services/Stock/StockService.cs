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
    public class StockService
    {

        // Only admins can use this Service
        //to do Authorize ROLES ADMIN

        private readonly MusicContext _db;

        public StockService(MusicContext db)
        {
            _db = db;
        }

        //respone request
        public async Task<StockViewModel> Create(StockViewModel request)
        {
            var stock = new Stock
            {
                Description = request.Description,
                Quantity = request.Quantity,
                ProductId = request.ProductId // wich stock the product is gonna relate to
            };

            _db.Stock.Add(stock);

           await _db.SaveChangesAsync();


            return new StockViewModel
            {
                Id = stock.Id,
                Description = stock.Description,
                Quantity = stock.Quantity
            };
        }

        public async Task<bool> DeleteStock(int id)
        {

            var stock = _db.Stock.FirstOrDefault(x => x.Id == id);
            _db.Stock.Remove(stock);

           await _db.SaveChangesAsync();
            return true;
        }


        //response request
        public async Task<StockViewModel> UpdateStock(StockViewModel request)
        {
            var stocks = new List<Stock>();

            foreach(var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    Description  = stock.Description,
                    Quantity = stock.Quantity,
                    ProductId = stock.ProductId
                    
                });
            }

            _db.Stock.UpdateRange(stocks);
            await _db.SaveChangesAsync();

            return new StockViewModel
            {
                Stock = request.Stock
            };
        }



        public IEnumerable<ProductViewModel> GetStock()
        {

            var stock = _db.Products
                .Include(x => x.Stock)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Stock = x.Stock.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Quantity = y.Quantity
                    })
                })
                .ToList();

            return stock;
        }



        

        
    }
}
