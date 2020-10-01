
using MusicData.Models;
using MusicWorld.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MusicWorld.Models
{
    public class CartViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int StockId { get; set; }
        public int Qty { get; set; }
        public Stock Stock { get; set; }
    }
}
