using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TBGFuelCalculator.Models;

namespace TBGFuelCalculator.Controllers
{
    public class HomeController : Controller
    {
        //Cents per litre
        private const int Discount = 10;


        private readonly ILogger<HomeController> _logger;

        // Calculator function
        // Reason for calculation - main variable to focus on is tank size.
        // The Tank size can be thought of as total amount of fuel. The more cars the more fuel. The greater tank size the more fuel.
        // Each amount of fuel is derived from carAmount * tank Size. Each liter of fuel 10c is saved.
        // Therefore cents total = fuelAmount * Discount(cents)
        // The return result must be in dollars, therefore the total Savings = calculatedDiscount/100. 
        private string calDiscount(int carAmount, int tankSize) {

            int discount = Discount;
            decimal calculatedDiscount = carAmount * discount * tankSize;
            decimal totalSavings = calculatedDiscount / 100;

            //Retured as a stringed message
            string msg = "The total savings is " + "$" + totalSavings.ToString();

            return msg;
        }


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //Just use the index action
        public IActionResult Index(int c, int t)
        {
            ViewBag.message = calDiscount(c, t);
            return View();
        }

        [HttpPost]
        public string PostData(int c, int t) {

           return calDiscount(c, t);

        }

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

