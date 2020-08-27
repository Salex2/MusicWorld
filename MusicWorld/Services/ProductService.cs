using MusicData;

using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicWorld.Services
{
    public class ProductService 
    {
        private readonly MusicContext _db;

        public ProductService(MusicContext db)
        {
            _db = db;
        }

        public async Task<Response> Create(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value
            };
            _db.Products.Add(product);

            await _db.SaveChangesAsync();

            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        

        public Product GetProduct(int id)
        {
          return  _db.Products.Where(x => x.Id == id).Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            }).FirstOrDefault();
        }

        public async Task<bool> Delete(int id)
        {
            var Product = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Products.Remove(Product);
            await  _db.SaveChangesAsync();
            return true;

            
        }

        public IEnumerable<Product> GetAllProducts() =>
           _db.Products.ToList().Select(x => new Product
           {
               Id = x.Id,
               Name = x.Name,  
               Value = x.Value
            });

        public async Task<Response> Update(Request request)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Value;


            await _db.SaveChangesAsync();
            return new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set;   }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set;  }
        
        }

       
    }
}
