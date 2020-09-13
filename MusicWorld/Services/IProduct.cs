using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MusicData.Models;


namespace MusicData.Models
{
    public interface IProduct
    {
        IEnumerable<ProductViewModel> GetProducts();
        ProductViewModel GetProduct(string name);




    }
}
