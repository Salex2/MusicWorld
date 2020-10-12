using Microsoft.AspNetCore.Http;
using MusicWorld.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services.Cart
{
    public class GetCustomerInformation
    {
        private ISession _session;

        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }

        public class Response
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }


            public string Adress1 { get; set; }

            public string Adress2 { get; set; }

            public string City { get; set; }

            public string PostCode { get; set; }
        }
       

        public Response Get()
        {
            var stringObject = _session.GetString("customer");

            if (String.IsNullOrEmpty(stringObject))
                return null;

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);


            return new Response
            {
                FirstName = customerInformation.FirstName,
                LastName = customerInformation.LastName,
                Email = customerInformation.Email,
                PhoneNumber = customerInformation.PhoneNumber,
                Adress1 = customerInformation.Adress1,
                City = customerInformation.City,
                Adress2 = customerInformation.Adress2,
                PostCode = customerInformation.PostCode,
            };
        }
    }
}
