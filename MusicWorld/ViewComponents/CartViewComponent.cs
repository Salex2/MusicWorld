using Microsoft.AspNetCore.Mvc;
using MusicData;
using MusicWorld.Services.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly MusicContext _db;

        public CartViewComponent(MusicContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string view = "Default") // when you pass the viewComponent somewhere you can specify the razor View; 
        {
            return View(view,new GetCart(HttpContext.Session, _db).Get());
        }
    }
}
