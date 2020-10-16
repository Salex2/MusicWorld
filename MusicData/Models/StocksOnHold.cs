using System;
using System.Collections.Generic;
using System.Text;

namespace MusicData.Models
{
    //we do this when we want to but the item in our cart ; Class for race condition when to users try to buy the same product 
    //we need this when we add to cart
    public class StocksOnHold
    {
        public int Id { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int Qty { get; set; }
        public DateTime ExpireDate { get; set; }


    }
}
