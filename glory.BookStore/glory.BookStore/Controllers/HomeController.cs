using glory.BookStore.Services;
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
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }


        public ViewResult Index()
        {
            var userId = _userService.GetUserId();//#102
            var isLoggedIn = _userService.IsAuthenticated();
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
