using System;
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
        public static decimal TotalPricesWithLyambda(this IEnumerable<Product> productEnum)
        {
            decimal total = 0;
            foreach (Product product in productEnum)
            {
                total += product.Price;
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

        public static IEnumerable<Product> FilterByLyambda(this IEnumerable<Product>
            productsEnum, Func<Product,bool> selectorParam )
        {
            foreach (Product product in productsEnum)
            {
                if (selectorParam(product))
                {
                    yield return product;
                }
            }
        }
    }
}