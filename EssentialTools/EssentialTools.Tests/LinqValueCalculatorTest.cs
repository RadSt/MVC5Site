using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class LinqValueCalculatorTest
    {
        private readonly Product[] products =
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
            var mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).
                Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            //Act
            var result = target.ValueProducts(products);

            //Assert
            Assert.AreEqual(products.Sum(e => e.Price), result);
        }

        private Product[] createProduct(decimal value)
        {
            return new[] {new Product {Price = value}};
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Thtough_Variable_Discounts()
        {
            //Arrange
            Mock<IDiscountHelper> mock=new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).
                Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).
                Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).
                Returns<decimal>(total=>total*0.9M);
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive))).
                Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);

            //Act
            decimal FiveDollarsDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarsDiscount = target.ValueProducts(createProduct(10));
            decimal FiftyDollarsDiscount = target.ValueProducts(createProduct(50));
            decimal HundredDollarsDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDolllarsDiscount = target.ValueProducts(createProduct(500));
            
            //Assert
            Assert.AreEqual(5,FiveDollarsDiscount,"$5 Fail");
            Assert.AreEqual(5,TenDollarsDiscount,"$10 Fail");
            Assert.AreEqual(45,FiftyDollarsDiscount,"$50 Fail");
            Assert.AreEqual(95,HundredDollarsDiscount,"$100 Fail");
            Assert.AreEqual(450,FiveHundredDolllarsDiscount,"$500 Fail");
            target.ValueProducts(createProduct(0));
        }
        
    }
}