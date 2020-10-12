
using MusicData.Models;
using MusicWorld.Services.Cart;
using MusicWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MusicWorld.Models
{
    public class CartViewModel
    {
        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int StockId { get; set; }
        public int Qty { get; set; }
        public Stock Stock { get; set; }
        public IEnumerable<CartViewModel> Cart { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public CustomerInformation CustomerInformation { get; set; }

       
    }
}
