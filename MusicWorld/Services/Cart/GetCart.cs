using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicData;
using MusicData.Models;
using MusicWorld.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services.Cart
{
    public class GetCart
    {
        private readonly ISession _session;
        private readonly MusicContext _db;

        public GetCart(ISession session, MusicContext db)
        {
            _session = session;
            _db = db;
        }


        public IEnumerable<CartViewModel> Get()
        {
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
                return new List<CartViewModel>();
            

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            var response = _db.Stock
                .Include(x => x.Product)
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new CartViewModel
                {
                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Value.ToString("N2")}",
                    StockId = x.Id,
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                })
                .ToList();

            return response;
        }
    }
}
