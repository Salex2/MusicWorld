﻿using MusicData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicWorld.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        
        

        public ICollection<Stock> Stock { get; set; } //Icollection and not List because we just need 
                                                      // to expose the Add and Remove of IEnumerable func.
        
    }
}
