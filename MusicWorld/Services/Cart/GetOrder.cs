using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicData;
using MusicData.Models;
using MusicWorld.Models;
using MusicWorld.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services.Cart
{
    public class GetOrder
    {
        private readonly ISession _session;
        private readonly MusicContext _db;

        public GetOrder(ISession session, MusicContext db)
        {
            _session = session;
            _db = db;
        }
  

        public OrderInformation Get()
        {
            var cart = _session.GetString("cart");
            
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var listOfProducts = _db.Stock
                .Include(x => x.Product)
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new OrderInformation
                {
                   ProductId = x.ProductId,
                   StockId = x.Id,
                   Value =  (int) (x.Product.Value * 100),
                   Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();

            var customerInfoString = _session.GetString("customer");

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(customerInfoString);


            return new OrderInformation
            {
                Products = listOfProducts,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    Email = customerInformation.Email,
                    PhoneNumber = customerInformation.PhoneNumber,
                    Adress1 = customerInformation.Adress1,
                    City = customerInformation.City,
                    Adress2 = customerInformation.Adress2,
                    PostCode = customerInformation.PostCode,
                }
            };
        }
    }
}
