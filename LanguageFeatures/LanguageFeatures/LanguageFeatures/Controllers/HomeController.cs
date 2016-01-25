using System;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Collections.Generic;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigateto a URL to URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            //Создать новый обьект Product
            Product myProduct=new Product();

            //Установить значения свойства
            myProduct.Name = "Kayak";

            //Получить значения свойства
            string productName = myProduct.Name;

            //Сгенерировать представление
            return View("Result", 
                (object) String.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            //Создать и заполнить новый обьект Product
            Product myProduct=new Product
            {
                ProductID = 100, Name = "Kayak",
                Description = "A boat for one person",
                Price  = 275M, Category = "Watersports"
            };
            return View("Result",
                (object) String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = {"apple", "orrange", "plum"};
            List<int> intList=new List<int>{10,20,30,40};
            Dictionary<string,int> myDict=new Dictionary<string, int>
            { {"apple",10},{"orrange",20},{"plum",30} };

            return View("Result", (object) stringArray[1]);
        }
    }
}