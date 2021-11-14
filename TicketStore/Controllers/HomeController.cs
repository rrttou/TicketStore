using Microsoft.AspNetCore.Authorization;
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
using Tweetinvi.Models;

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
            string APIkey = "YWfc6WQSoP77w21A8RXn11MKi";
            string APIsecret = "7KZp8bHnrQsQnYf1WRWdSj5Z0Q3uFIypikUOUYfUyu8dcnuBgP";
            string APIToken = "1459439825635975171-KWhMok4VubP8bi1RFbpmLpnrlhd73z";
            string APITokenSecret = "1lU5pAOPLxV0ULIS8cTKxW0hHueKNFbqqed7OQtu054y1";
            string APIBearerToken = "AAAAAAAAAAAAAAAAAAAAAA0iVwEAAAAAFA35awVanGeXFekdD77H87WStVQ%3Dea6SluyLrA2FoHdCAkOUK3htJS8ukwjPjsUQIlofvV8opgqVpj";
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
