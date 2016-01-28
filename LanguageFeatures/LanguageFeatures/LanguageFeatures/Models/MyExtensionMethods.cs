using System.Collections.Generic;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> productEnum)
        {
            decimal total = 0;
            foreach (Product prod in productEnum)
            {
                total += prod.Price;
            }
            return total;
        }

        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product>
            productEnum, string categoryParam)
        {
            foreach (Product product in productEnum)
            {
                if(product.Category == categoryParam)
                {
                    yield return product;
                }
            }
        }
    }
}