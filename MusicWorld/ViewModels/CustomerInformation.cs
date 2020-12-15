
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MusicWorld.Services.Cart.CreateOrder;

namespace MusicWorld.ViewModels
{
    public class CustomerInformation
    {

        public string SessionId { get; set; }
        public List<Stock> Stocks { get; set;  }
        public string StripeReference { get; set; }
     
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        
        public string Adress1 { get; set; }

        public string Adress2 { get; set; }
       
        public string City { get; set; }
       
        public string PostCode { get; set; }
    }
}
