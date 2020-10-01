using System;
using System.Collections.Generic;
using System.Text;

namespace MusicData.Models
{
    /// <summary>
    /// It will be stored in the session.
    /// </summary>
    public class CartProduct
    {
        public int StockId { get; set; }
        public int Qty { get; set; }
    }
}
