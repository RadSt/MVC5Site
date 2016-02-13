using System;
using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class LinqValueCalculatorTest
    {
        private Product[] products =
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner Flag", Category = "Soccer", Price = 34.95M}
        };
        [TestMethod]
        public void Sum_Products_Correctly()
        {
            //Arrange
            Mock<IDiscountHelper> mock=new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<Decimal>())).
                Returns<decimal>(total => total);
            var target=new LinqValueCalculator(mock.Object);

            //Act
            var result = target.ValueProducts(products);

            //Assert
            Assert.AreEqual(products.Sum(e=>e.Price), result);
        }
    }
}
