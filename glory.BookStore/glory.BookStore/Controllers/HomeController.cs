using glory.BookStore.Models;
using glory.BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Controllers
{
    
    //[Route("~/")]
    public class HomeController:Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }


        public async Task<ViewResult> Index()
        {
            //#105
            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string> { "test@gmail.com" }
            };
            await _emailService.SendTestEmail(options);

            //var userId = _userService.GetUserId();//#102
            //var isLoggedIn = _userService.IsAuthenticated();
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
