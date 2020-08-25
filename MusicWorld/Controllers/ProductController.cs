using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicData;

using MusicWorld.Models;
using MusicWorld.Services;

namespace MusicWorld.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {

        private readonly MusicContext _db;
        

        public ProductController(MusicContext db)
        {
            _db = db;
            
        }


        




    
        }
    }
