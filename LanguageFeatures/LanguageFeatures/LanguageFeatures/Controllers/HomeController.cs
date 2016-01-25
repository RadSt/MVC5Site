using System;
using System.Web.Mvc;
using LanguageFeatures.Models;

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
    }
}