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


        public CartViewModel Get()
        {
            var stringObject = _session.GetString("cart");

            var cartProduct = JsonConvert.DeserializeObject<CartProduct>(stringObject);

            var response = _db.Stock
                .Include(x => x.Product)
                .Where(x => x.Id == cartProduct.StockId)
                .Select(x => new CartViewModel
                {
                    Name = x.Product.Name,
                    Value = $"$ {x.Product.Value.ToString("N2")}",
                    StockId = x.Id,
                    Qty = cartProduct.Qty
                })
                .FirstOrDefault();

            return response;
        }
    }
}
