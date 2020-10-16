using MusicWorld.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MusicData.Models;
using System.Threading.Tasks;

namespace MusicData.Models
{
    public interface IProduct
    {
        IEnumerable<ProductViewModel> GetProducts();
        Task<ProductViewModel> GetProduct(string name);

    }
}
