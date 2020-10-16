using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class AddToCart
    {
        private  ISession _session;
        private readonly MusicContext _db;

        public AddToCart(ISession session, MusicContext db)
        {
            _session = session;
            _db = db;
        }


        public async Task<bool> Add(CartViewModel request)
        {



            //the stock that we want to hold
            var stockToHold = _db.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();

            if(stockToHold.Quantity < request.Qty)
            {
                return false;
            }

            _db.StocksOnHold.Add(new StocksOnHold
            {
                StockId = stockToHold.Id,
                Qty = request.Qty,
                ExpireDate = DateTime.Now.AddMinutes(20)
            });

            // 
            stockToHold.Quantity = stockToHold.Quantity - request.Qty;

            await _db.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            //check if the cart is not empty - we deserialize only if is not empty
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            //check if there is a stock that we are already adding in case we need to append the qty

            if (cartList.Any(x => x.StockId == request.StockId))
            {
                //find this stock in our cart and append the qty
                cartList.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }


            stringObject = JsonConvert.SerializeObject(cartList);

            //Add the cart to session
            _session.SetString("cart", stringObject);

            return true;
        }
    }
}
