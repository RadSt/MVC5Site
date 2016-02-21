using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product!=null)
            {
                GetCart().AddItem(product,1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoweFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product!=null)
            {
                GetCart().RemoweLine(product);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        private Cart GetCart()
        {
            //Состояние сеанса(позволяет ассоциировать данные с сеансом) 
            // состояние класса Cart
            Cart cart = (Cart) Session["Cart"];
            if (cart==null)
            {
                cart=new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}