using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AzureTest.Models;
using Microsoft.AspNetCore.Authorization;

namespace AzureTest.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration config { get; set; }
        public HomeController(IConfiguration configuration )
        {
            this.config = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {

            ViewData["Message"] = this.config["Greeting"];

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
