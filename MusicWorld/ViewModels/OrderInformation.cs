using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.ViewModels
{
    public class OrderInformation
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int StockId { get; set; }
        public int Value { get; set; }

        public int Id { get; set; }
        public string OrderRef { get; set; }
        public string StripeReference { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public string TotalValue { get; set; }

        public IEnumerable<ProductViewModel> Productss { get; set; }

        public IEnumerable<OrderInformation> Products { get; set; }
        public CustomerInformation CustomerInformation { get; set; }

        public int GetTotalCharge() => Products.Sum(x => x.Value * x.Qty);
    }
}
