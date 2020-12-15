using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCasino.Service.Abstract;
using WebCasino.Web.Models;

namespace WebCasino.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICurrencyRateApiService currencyService;

        public HomeController(ICurrencyRateApiService currencyService)
        {
            this.currencyService = currencyService;
        }

        public IActionResult Index()
        {
          
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Invalid()
        {
            return View();
        }

        public IActionResult ServerError()
        {
            return View();
        }
    }
}
