using System;
using System.Web.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            //Проверка правильности введенных данных
            if (ModelState.IsValid)
            {
                //Чтото сделать: отправить guestResponse по эл. почте организатору 
                //вечеринки
                return View("Thanks", guestResponse);
            }
            // В случае ошибки данных 
            return View();
        }
    }
}