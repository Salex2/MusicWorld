using Microsoft.AspNetCore.Http;
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
            var cartProduct = new CartProduct
            {
                StockId = request.StockId,
                Qty = request.Qty
            };

            var stringObject = JsonConvert.SerializeObject(cartProduct);


            _session.SetString("cart", stringObject);


        }
    }
}
