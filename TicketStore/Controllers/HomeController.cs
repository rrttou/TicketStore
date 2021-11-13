﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicketStore.Data;
using TicketStore.Models;
using Tweetinvi;

namespace TicketStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShowContext _context;

 
        public HomeController(ILogger<HomeController> logger, ShowContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var events = from t in _context.Event select t;
            if (events.Any())
            {
                events = events.OrderBy(a => a.Date);
                return View(events.First());
            }
            return View();
           
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login");
        }


        public async Task<IActionResult> Twitter()
        {
            if (!(User.Claims.Any() && User.Claims.First(c => c.Type == "Role").Value.Equals("Admin")))
            {
                return View("NotFound");
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Twitter(String tweet)
        {
            string APIkey = "xRHUJp5A6hxRNw8XfjKnCjMBv";
            string APIsecret = "9jPnJN3rrBtIqSwrKIJPQf3y5hPPqiUvZ1s5CIhBJPw7V9tttI";
            string APIToken = "1459045772188606486-Rolj7jq0sF37UavkOY9WrbMpFElo1r";
            string APITokenSecret = "qHy2O4QM7e95ftCJgSWL9kR7yUMbewk3d0lGH2Tlb9e5p";
            string APIBearerToken = "AAAAAAAAAAAAAAAAAAAAAKz9VgEAAAAAkR%2Bb%2FAczCSBT4qtI9we0zEYQqrc%3DyXaxLxF5upOVK7BOG2mDCYE0YHWvf93teaociiiJQHHNe9x1To";
            var client = new TwitterClient(APIkey, APIsecret, APIToken, APITokenSecret);
            //client.Config.TweetMode = TweetMode.Compat;
            await client.Tweets.PublishTweetAsync(tweet);
            return View();
        }

        public IActionResult About()
        {
            return View("About");
        }
        public IActionResult Contact()
        {
            return View("Contact");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
