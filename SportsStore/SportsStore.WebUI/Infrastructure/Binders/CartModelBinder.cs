using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder:IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            Cart cart = null;
            //Получить обьект Cart из сеанса
            if (controllerContext.HttpContext.Session !=null)
            {
                cart = (Cart) controllerContext.HttpContext.Session[sessionKey];
            }
            //Создать экземпляр Cart, если он не обнаружен в данных сеанса
            if (cart==null)
            {
                cart=new Cart();
                if (controllerContext.HttpContext.Session !=null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            return cart;
        }
    }
}