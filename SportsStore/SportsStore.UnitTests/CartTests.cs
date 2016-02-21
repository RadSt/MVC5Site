using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //Arrange
            Product p1=new Product{ ProductID = 1, Name = "P1" };
            Product p2=new Product{ ProductID = 2, Name = "P2" };
            Cart target=new Cart();

            
            //Act
            target.AddItem(p1,1);
            target.AddItem(p2,1);
            CartLine[] result = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(result.Length,2);
            Assert.AreEqual(result[0].Product,p1);
            Assert.AreEqual(result[1].Product,p2);
        }

        [TestMethod]
        public void Add_Quantity_For_Existing_Lines()
        {
            //Arrange
            Product p1=new Product{ProductID = 1,Name="P1"};
            Product p2=new Product{ProductID = 2,Name = "P2"};
            Cart target=new Cart();

            //Act
            target.AddItem(p1,1);
            target.AddItem(p2,1);
            target.AddItem(p1,10);
            CartLine[] result = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(result.Length,2);
            Assert.AreEqual(result[0].Quantity,11);
            Assert.AreEqual(result[1].Quantity,1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            //Arrange
            Product p1=new Product{ProductID = 1,Name="P1"};
            Product p2=new Product{ProductID = 2,Name="P2"};
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            Cart target = new Cart();

            //Act
            target.AddItem(p1,1);
            target.AddItem(p2,3);
            target.AddItem(p3,5);
            target.AddItem(p2,1);
            target.RemoweLine(p2);

            //Assert 
            Assert.AreEqual(target.Lines.Where(p=>p.Product==p2).Count(),0);
            Assert.AreEqual(target.Lines.Count(),2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //Arrange
            Product p1=new Product{ProductID = 1,Name = "P1",Price = 100M};
            Product p2=new Product{ProductID = 2,Name="P2",Price = 50M};
            Cart target=new Cart();

            //Act
            target.AddItem(p1,2);
            target.AddItem(p2,2);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result,300M);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Arrange
            Product p1=new Product{ProductID = 1,Name = "P1",Price = 100M};
            Product p2=new Product{ProductID = 2,Name = "P2",Price = 50M};
            Cart target=new Cart();

            //Act
            target.AddItem(p1,1);
            target.AddItem(p2,3);
            target.AddItem(p1,5);
            target.Clear();

            //Assert
            Assert.AreEqual(target.Lines.Count(),0);
        }
    }
}
