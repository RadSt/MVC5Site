﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            });
            AdminController target=new AdminController(mock.Object);

            //Act
            Product[] result = ((IEnumerable<Product>) target.Index().ViewData.Model)
                .ToArray();

            //Assert
            Assert.AreEqual(result.Length,3);
            Assert.AreEqual("P1",result[0].Name);
            Assert.AreEqual("P2",result[1].Name);
            Assert.AreEqual("P3",result[2].Name);

        }

        [TestMethod]
        public void Can_edit_Product()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });
            AdminController target=new AdminController(mock.Object);

            //Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            //Assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexisting_Product()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            });
            AdminController target=new AdminController(mock.Object);

            //Act
            Product result= target.Edit(4).ViewData.Model as Product;

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            AdminController target=new AdminController(mock.Object);
            Product product=new Product {Name = "Test"};


            //Act
            ActionResult result = target.Edit(product);

            //Assert
            mock.Verify(m=>m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result,typeof(ViewResult));

        }

        [TestMethod]
        public void Save_Invalid_Changes()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            AdminController target=new AdminController(mock.Object);
            Product product=new Product{Name = "Test"};
            target.ModelState.AddModelError("error","error");

            //Act
            ActionResult result = target.Edit(product);

            //Assert
            mock.Verify(m=>m.SaveProduct(It.IsAny<Product>()),Times.Never());
            Assert.IsInstanceOfType(result,typeof(ViewResult));

        }

        [TestMethod]
        public void Can_Delete_Valid_Product()
        {
            //Arrange
            Product prod=new Product{ProductID = 2,Name="Test"};
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 3, Name = "P3"}
            });
            AdminController target=new AdminController(mock.Object);
            
            //Act
            target.Delete(prod.ProductID);

            //Assert
            mock.Verify(m=>m.DeleteProduct(prod.ProductID));
        }
    }
}
