using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Controllers
{
    [Route("blog")]
    public class Blog : Controller
    {
      [Route("")]
      public IActionResult Index()
        {
            return View();
        }
    }
}
