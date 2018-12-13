using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreVideo.Models;

namespace AspNetCoreVideo.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var model = new Video { Id = 1, Title = "Shrek" };
            return View(model);
        }
    }
}
