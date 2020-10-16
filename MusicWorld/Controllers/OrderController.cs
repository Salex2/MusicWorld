using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicData;
using MusicWorld.Services.Cart;
using MusicWorld.ViewModels;
using Stripe;

namespace MusicWorld.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly MusicContext _db;

        public OrderController(MusicContext db,  IConfiguration config)
        {
            string PublicKey = config["Stripe:PublicKey"].ToString();
            _db = db;
        }
        
        [HttpGet]
        [Route("UserOrder/{reference}")]
        public IActionResult UserOrder(string reference)
        {
           OrderInformation Order = new GetUserOrder(_db).GetUserOrders(reference);

            return View(Order);
        }



        //STRIPE PAYMENT
        [HttpGet]
        [Route("Payment")]
        public IActionResult Payment()
        {

            var information = new GetCustomerInformation(HttpContext.Session).Get();

            if (information == null)
            {
                return RedirectToAction("CustomerInformation", "Product");
            }

            return View();
        }

        [HttpPost]
        [Route("Payment")]
        public async Task<IActionResult> Payment(string stripeEmail, string stripeToken)
        {
            //stripe custom classes

            var customers = new CustomerService();
            var charges = new ChargeService();

            var CartOrder = new GetOrder(HttpContext.Session, _db).Get();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = CartOrder.GetTotalCharge(),
                Description = "Shop Purchase",
                Currency = "USD",
                Customer = customer.Id
            });

            await new CreateOrder(_db).Create(new CustomerInformation
            {
                StripeReference = charge.Id,

                FirstName = CartOrder.CustomerInformation.FirstName,
                LastName = CartOrder.CustomerInformation.LastName,
                Email = CartOrder.CustomerInformation.Email,
                PhoneNumber = CartOrder.CustomerInformation.PhoneNumber,
                Adress1 = CartOrder.CustomerInformation.Adress1,
                Adress2 = CartOrder.CustomerInformation.Adress2,
                City = CartOrder.CustomerInformation.City,
                PostCode = CartOrder.CustomerInformation.PostCode,

                Stocks = CartOrder.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Qty = x.Qty

                }).ToList()

            });


            return View();
        }
    }
}