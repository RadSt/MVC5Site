using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        // Обязательное для заполения поле с именем
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        //Обязательное для заполнения 1 поле с адресом
        [Required(ErrorMessage = "Please enter the first adress line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        //Обязательное поле для названия города
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
        //Обязательное поле для штата
        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        //Обязательное поле для названия страны
        public string Country { get; set; }
        public bool GiftWrap { get; set; }

    }
}