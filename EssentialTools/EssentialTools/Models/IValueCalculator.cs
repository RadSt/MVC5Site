using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace EssentialTools.Models
{
    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);
    }
}