using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
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

        public AddToCart(ISession session)
        {
            _session = session;
        }


        public void Add(CartViewModel request)
        {
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


        }
    }
}
