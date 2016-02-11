using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var cartTotal = products.TotalPrices();
            var arrayTotal = productArray.TotalPrices();

            return View("Result",
                (object) string.Format("CartTotal: {0:c}, ArrayTotal: {1:c}"
                    , cartTotal, arrayTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            //Использование фильтрующего метода
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Kayak",
                        Category = "Watersports",
                        Price = 275M
                    },
                    new Product
                    {
                        Name = "LifeJacket",
                        Category = "Watersports",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Soccer ball",
                        Category = "Soccer"
                        ,
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner flag",
                        Category = "Soccer",
                        Price = 34.95M
                    }
                }
            };
            decimal total = 0;
            foreach (var product in products.FilterByCategory("Soccer"))
            {
                total += product.Price;
            }
            return View("Result", (object) string.Format("Total: {0}", total));
        }

        public ViewResult UseFilterExtensionFumMethod()
        {
            //Создать и заполнить обьект shoppingCard
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Kayak",
                        Category = "Watersports",
                        Price = 275M
                    },
                    new Product
                    {
                        Name = "LifeJacket",
                        Category = "Watersports",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Soccer ball",
                        Category = "Soccer"
                        ,
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner flag",
                        Category = "Soccer",
                        Price = 34.95M
                    }
                }
            };
            decimal total = 0;
            //Лямда выражение в foreach
            foreach (var product in products.FilterByLyambda(product =>
                product.Category == "Socer" || product.Price > 20))
            {
                total += product.Price;
            }

            return View("Result", (object) string.Format("Total: {0:c}", total));
        }

        public ViewResult CreateAnonArray()
        {
            //Обьявление массива без явного указания типа
            var oddsAndEnds = new[]
            {
                new {Name = "MVC", Cattegory = "Pattern"},
                new {Name = "Hat", Cattegory = "Clothing"},
                new {Name = "Apple", Cattegory = "Fruit"}
            };

            var result = new StringBuilder();

            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }

            return View("Result", (object) result.ToString());
        }

        public ViewResult FindProducts()
        {
            //Использование LINQ
            Product[] products =
            {
                new Product
                {
                    Name = "Kayak",
                    Category = "Watersports",
                    Price = 275M
                },
                new Product
                {
                    Name = "LifeJacket",
                    Category = "Watersports",
                    Price = 48.95M
                },
                new Product
                {
                    Name = "Soccer ball",
                    Category = "Soccer"
                    ,
                    Price = 19.50M
                },
                new Product
                {
                    Name = "Corner flag",
                    Category = "Soccer",
                    Price = 34.95M
                }
            };
            //LINQ NOTATION
            //var foundProduct = from match in products
            //    orderby match.Price descending
            //    select new {match.Name, match.Price};
            //LINQ DOT NOTATION
            var foundProduct = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new {e.Name, e.Price});

            int count = 0;
            StringBuilder result=new StringBuilder();
            foreach (var product in foundProduct)
            {
                result.AppendFormat("Price {0:c}", product.Price);
                if (++count==3)
                {
                    break;
                }
            }
            return View("Result", (object) result.ToString());
        }
    }
}