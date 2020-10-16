using MusicData.Models;
using MusicWorld.Models.Products;
using MusicWorld.Services;
using MusicWorld.Services.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Models
{

    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Write Product Name")]
        [StringLength(100, MinimumLength = 5,
        ErrorMessage = "Name must be between 5 and 100 characters long")]
        public string Name { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Description must be at least 20 characters long")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Value { get; set; }

       

        public int Id { get; set; }

        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }

        public string StockDescription { get; set; }

        public IEnumerable<Product> Product { get; set; }
         
        public IEnumerable<StockViewModel> Stock { get; set; }

        public CartViewModel CartViewModel { get; set; }
        
       

        
        
        
        




    }
}
