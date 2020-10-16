using Microsoft.EntityFrameworkCore;
using MusicData;
using MusicData.Models;
using MusicWorld.Models;
using MusicWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services.Cart
{
    //we want to take a order and include all of its order products,include the stock and product information
    public class GetUserOrder
    {
        private readonly MusicContext _db;

        public GetUserOrder(MusicContext db)
        {
            _db = db;
        }

        //we use the order refference

        public OrderInformation GetUserOrders(string reference)
        {
          return  _db.Orders
                .Where(x => x.OrderRef == reference)
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .Select(x => new OrderInformation
                {
                    OrderRef = x.OrderRef,
                    

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Adress1 = x.Adress1,
                    Adress2 = x.Adress2,
                    City = x.City,
                    PostCode = x.PostCode,

                    Productss = x.OrderStocks.Select(y => new ProductViewModel
                    {
                        Name = y.Stock.Product.Name,
                        Description = y.Stock.Product.Description,
                        Value = $"$ { y.Stock.Product.Value.ToString("N2")}",
                        Qty = y.Qty,
                        StockDescription = y.Stock.Description
                    }),

                    TotalValue = x.OrderStocks.Sum(y => y.Stock.Product.Value).ToString("N2")
                })
                .FirstOrDefault();
        }


    }
}
