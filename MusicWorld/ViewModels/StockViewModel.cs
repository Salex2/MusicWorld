using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Models.Products
{
    public class StockViewModel
    {
        public IEnumerable<StockViewModel> Stock { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public bool InStock { get; set; }
    }
}
