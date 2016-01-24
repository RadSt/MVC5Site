using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Please enter your name")]
        //Проверка значения и вывод ошибок.
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+",
            ErrorMessage = "Please enter a vallid email address")]
        //Проверка адресса эл. почты регуляркой
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        //Проверка номера телефона
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please specify whether yoll attend")]
        //Проверка на выбор из списка
        public bool? WillAttend { get; set; }
        //применен nullable тип для того чтобы можно было применить сред-во Required
    }
}