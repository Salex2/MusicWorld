﻿using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicData.Models
{
    public class OrderProduct //Link between our Order and Product
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Qty { get; set; }//how much of that product the user purchased
        public int StockId { get; set; }

        public Stock Stock { get; set; } //link to the stock
    }
}
