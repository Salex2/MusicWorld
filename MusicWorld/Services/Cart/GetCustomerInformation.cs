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


        public CustomerInformation Get()
        {
            var stringObject = _session.GetString("customer");

            if (String.IsNullOrEmpty(stringObject))
                return null;

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);


            return new CustomerInformation
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
