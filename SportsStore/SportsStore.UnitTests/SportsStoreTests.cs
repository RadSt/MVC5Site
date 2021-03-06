﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Instrumentation;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class SportsStoreTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller=new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductListViewModel result = (ProductListViewModel) controller.List(null,2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length==2);
            Assert.AreEqual(prodArray[0].Name,"P4");
            Assert.AreEqual(prodArray[1].Name,"P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            //Организация -определение вспомогательного метода HTML;
            //Это необходимо для применения расширяющего метода
            HtmlHelper myHelper = null;
            //Организация создания данных PageInfo
            PagingInfo pagingInfo=new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            //Организация-настройка делегата с помощью лямбда выражения
            Func<int, string> pageUriDelegate = i => "Page" + i;

            //Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUriDelegate);

            //Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",result.ToString());
        }

        [TestMethod]
        public void Can_SendPagination_View_Model()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller=new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductListViewModel result = (ProductListViewModel) controller.List(null,2).Model;

            //Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage,2);
            Assert.AreEqual(pagingInfo.ItemsPerPage,3);
            Assert.AreEqual(pagingInfo.TotalItems,5);
            Assert.AreEqual(pagingInfo.TotalPages,2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID = 1, Name = "P1",Category = "Cat1"}, 
                new Product{ProductID = 2,Name = "P2",Category = "Cat2"},
                new Product{ProductID = 3,Name = "P3",Category = "Cat1"}, 
                new Product{ProductID = 3,Name = "P4",Category = "Cat2"}, 
                new Product{ProductID = 5,Name = "P5",Category = "Cat3"}, 
            });
            ProductController controller=new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            Product[] result = ((ProductListViewModel)controller.List("Cat2", 1)
                .Model).Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length,2);
            Assert.IsTrue(result[0].Name=="P2" && result[0].Category=="Cat2");
            Assert.IsTrue(result[1].Name=="P4" && result[1].Category=="Cat2");

        }

        [TestMethod]
        public void Can_Create_categories()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID = 1, Name = "P1", Category = "Oranges"},
                new Product{ProductID = 2, Name = "P2", Category = "Apples"},
                new Product{ProductID = 3,Name = "P3", Category = "Cucumber"},
                new Product{ProductID = 4, Name = "P4",Category = "Apples"},
                new Product{ProductID = 5, Name = "P5", Category = "Oranges"} 
            });
            NavController target=new NavController(mock.Object);

            //Act
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(results.Length,3);
            Assert.AreEqual(results[0],"Apples");
            Assert.AreEqual(results[1],"Cucumber");
            Assert.AreEqual(results[2],"Oranges");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m=>m.Products).Returns(new Product[]
            {
                new Product{ProductID = 1,Name = "P1",Category = "Apples"},
                new Product{ProductID = 2, Name = "P2", Category = "Oranges"} 
            });
            NavController target=new NavController(mock.Object);
            string categoryToSelect = "Apples";

            //Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect,result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange
            Mock<IProductRepository> mock=new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product{ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product{ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product{ProductID = 4, Name = "P4", Category = "Cat2"}, 
                new Product{ProductID = 5, Name = "P5", Category = "Cat3"} 
            });
            ProductController target=new ProductController(mock.Object);
            target.PageSize = 3;

            //Act
            int res1 = ((ProductListViewModel)target
                .List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductListViewModel) target
                .List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductListViewModel) target
                .List("Cat3").Model).PagingInfo.TotalItems;
            int resall = ((ProductListViewModel) target
                .List(null).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(res1,2);
            Assert.AreEqual(res2,2);
            Assert.AreEqual(res3,1);
            Assert.AreEqual(resall,5);
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange
            Mock<IOrderProcessor> mock=new Mock<IOrderProcessor>();
            Cart cart=new Cart();
            ShippingDetails shippingDetails=new ShippingDetails();
            CartController target = new CartController(null, mock.Object);

            //Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            //Assert
            //Утверждение что заказ не был передан обработчику (метод не вызывается)
            mock.Verify(m=>m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());

            Assert.AreEqual("",result.ViewName);
            Assert.AreEqual(false,result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_Shipping_Details()
        {
            //Arrange
            Mock<IOrderProcessor> mock=new Mock<IOrderProcessor>();
            Cart cart=new Cart();
            cart.AddItem(new Product(), 1);
            CartController target=new CartController(null,mock.Object);
            target.ModelState.AddModelError("error","error");

            //Act
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //Assert
            mock.Verify(m=>m.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingDetails>()),
                Times.Never());

            Assert.AreEqual("",result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            //Arrange
            Mock<IOrderProcessor> mock=new Mock<IOrderProcessor>();
            Cart cart=new Cart();
            cart.AddItem(new Product(), 1);
            CartController target=new CartController(null,mock.Object);

            //Act
            ViewResult result = target.Checkout(cart, new ShippingDetails());
            
            //Assert
            mock.Verify(m=>m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true,result.ViewData.ModelState.IsValid);
        }
    }
}
