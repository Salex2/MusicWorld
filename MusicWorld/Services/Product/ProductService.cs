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

        


        //product Create,delete,update, getAllProducts,GetProduct

        public async Task<ProductViewModel> Create(ProductViewModel request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Valuee
            };
            _db.Products.Add(product);

            await _db.SaveChangesAsync();

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Valuee = product.Value

            };
        }

        
        //product class
        public ProductViewModel GetProduct(int id)
        {
          return  _db.Products.Where(x => x.Id == id).Select(x => new ProductViewModel
          {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Valuee = x.Value
          }).FirstOrDefault();
        }

        public async Task<bool> Delete(int id)
        {
            var Product = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Products.Remove(Product);
            await  _db.SaveChangesAsync();
            return true;

            
        }

        
        //product class
        public IEnumerable<ProductViewModel> GetAllProducts() =>
           _db.Products.ToList().Select(x => new ProductViewModel
           {
               Id = x.Id,
               Name = x.Name,  
               Valuee = x.Value
           });
        //request class
        public async Task<ProductViewModel> Update(ProductViewModel request)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Value = request.Valuee;


            await _db.SaveChangesAsync();
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Valuee = product.Value
            };
        }

       
        
        


       
    }
}
