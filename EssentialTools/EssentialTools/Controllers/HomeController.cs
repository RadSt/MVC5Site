﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // обьявление интерфейса для Ninject
        private IValueCalculator calc;
        private Product[] products=
        {
            new Product{Name="Kayak",Category = "Watersport",Price = 275M},
            new Product{Name="Lifejacket",Category = "Watersports",Price = 48.95M},
            new Product{Name="Soccer Ball",Category = "Soccer",Price = 19.50M},
            new Product{Name="Corner flag",Category = "Soccer",Price = 34.95M} 
        };
        public HomeController(IValueCalculator calcParam, IValueCalculator calcPram2)
        {
            // Добавление интерфейса с Ninject
            calc = calcParam;
        }
        public ActionResult Index()
        {
            ShoppingCart cart=new ShoppingCart(calc){Products = products};
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}