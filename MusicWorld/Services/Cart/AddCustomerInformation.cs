using Microsoft.AspNetCore.Http;
using MusicData.Models;
using MusicWorld.Models;
using MusicWorld.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Services.Cart
{
    public class AddCustomerInformation // we want to store customer information in cache
    {
        private ISession _session;

        public AddCustomerInformation(ISession session)
        {
            _session = session;
        }


        public void Add(CustomerViewModel request)
        {
            var customerInformation = new CustomerInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Adress1 = request.Adress1,
                City = request.City,
                Adress2 = request.Adress2,
                PostCode = request.PostCode,
            };

            var  stringObject = JsonConvert.SerializeObject(customerInformation);

        
            //add customer information to session
            _session.SetString("customer", stringObject);

        }
    }
}
