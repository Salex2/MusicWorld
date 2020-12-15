using MusicData;
using MusicData.Models;
using MusicWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services.Cart
{
    public class CreateOrder
    {
        private MusicContext _db;

        public CreateOrder(MusicContext db)
        {
            _db = db;
        }

        public class Stock
        { 
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public async Task<bool> Create(CustomerInformation request)
        {
            //update the stocks from db
            var stockOnHold = _db.StocksOnHold.Where(x => x.SessionId == request.SessionId).ToList();

            //remove the stock we are holding for this customer
            _db.StocksOnHold.RemoveRange(stockOnHold); 
           

            var order = new Order
            {
                OrderRef = CreateOrderReference(),
                StripeReference = request.StripeReference,

                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Adress1 = request.Adress1,
                Adress2 = request.Adress2,
                City = request.City,
                PostCode = request.PostCode,

                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Qty = x.Qty

                }).ToList()

            };

            _db.Orders.Add(order);

           return await _db.SaveChangesAsync() > 0;

      
        }

        public string CreateOrderReference()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new char[12];
            var random = new Random();

            do
            {
                for (int i = 0; i < result.Length; i++)
                    result[i] = chars[random.Next(chars.Length)];
            } while (_db.Orders.Any(x => x.OrderRef == new string(result)));
            
        

            return new string(result);
        }
    }
}
