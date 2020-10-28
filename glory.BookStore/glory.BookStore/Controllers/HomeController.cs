using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Controllers
{
    
    public class HomeController:Controller
    {


        public ViewResult Index()
        {
            return View();
        }

        [Route("~/about-us")]  //yeni url
        public ViewResult AboutUs()
        {
            return View();
        }
        [Route("~/contact-us")] //yeni url
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
