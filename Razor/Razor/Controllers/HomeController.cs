﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razor.Models;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Product myProduct=new Product
        {
            ProductID = 1,
            Name = "Kayak",
            Description = "A board for one person",
            Category = "Watersports",
            Price=275M
        };
        public ActionResult Index()
        {
            return View(myProduct);
        }
        public ActionResult NameAndPrice()
        {
            return View(myProduct);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            return View(myProduct);
        }
    }
}