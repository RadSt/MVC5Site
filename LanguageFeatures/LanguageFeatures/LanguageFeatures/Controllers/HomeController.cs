using System.Collections.Generic;
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
            var myProduct = new Product();

            //Установить значения свойства
            myProduct.Name = "Kayak";

            //Получить значения свойства
            var productName = myProduct.Name;

            //Сгенерировать представление
            return View("Result",
                (object) string.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            //Создать и заполнить новый обьект Product
            var myProduct = new Product
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
            return View("Result",
                (object) string.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = {"apple", "orrange", "plum"};
            var intList = new List<int> {10, 20, 30, 40};
            var myDict = new Dictionary<string, int>
            {{"apple", 10}, {"orrange", 20}, {"plum", 30}};

            return View("Result", (object) stringArray[1]);
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "LifeJacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                }
            };

            //Создать и заполнить массив обьектов Product
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "LifeJacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };
            // Получить общую стоимость товаров в корзине
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();

            return View("Result",
                (object) string.Format("CartTotal: {0:c}, ArrayTotal: {1:c}"
                , cartTotal,arrayTotal));
        }
    }
}