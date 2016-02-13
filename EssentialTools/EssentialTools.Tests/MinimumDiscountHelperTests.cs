using System;
using System.Runtime.Remoting;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EssentialTools.Tests
{
    [TestClass]
    public class MinimumDiscountHelperTests
    {
        private IDiscountHelper getTestObject()
        {
            return new MinimumDiscountHelper();
        }
        [TestMethod]
        public void Discount_Above_100 ()
        {
            //Arrange
            IDiscountHelper target = getTestObject();
            decimal total = 200;
            
            //Act
            var discountedTotal = target.ApplyDiscount(total);

            //Assertion
            Assert.AreEqual(total*0.9M,discountedTotal);
        }

        [TestMethod]
        public void Discount_Between_10_and_100()
        {
            //Arrange
            IDiscountHelper target = getTestObject();

            //Act
            decimal tenDollarsDiscount = target.ApplyDiscount(10);
            decimal hundredDollarsDiscount = target.ApplyDiscount(100);
            decimal fiftyDollarsDiscount = target.ApplyDiscount(50);

            //Assertion
            Assert.AreEqual(5, tenDollarsDiscount, "$10 discount is wrong");
            Assert.AreEqual(95,hundredDollarsDiscount,"$100 discount is wrong");
            Assert.AreEqual(45,fiftyDollarsDiscount,"$50 discount is wrong");
        }

        [TestMethod]
        public void Discount_Less_Than_10()
        {
            //Arrange
            IDiscountHelper target = getTestObject();

            //Act
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);

            //Assertion
            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0,discount0);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            //Arrange
            IDiscountHelper target = getTestObject();

            //Act
            target.ApplyDiscount(-1);
        }
    }
}
