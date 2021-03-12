using System;
using Dy.Northwind.Business.Concrete.Managers;
using Dy.Northwind.DataAccess.Abstract;
using Dy.Northwind.DataAccess.Concrete.EntityFramework;
using Dy.Northwind.Entities.Concrete;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dy.Northwind.Bussiness.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        //[ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void Product_validation_check()
        {
            Mock<IProductDal> mock = new Mock<IProductDal>();
            ProductManager produtctManager = new ProductManager(new EfProductDal());

            produtctManager.Add(new Product { CategoryId = 1, ProductName = "IPhone", QuantityPerUnit = "50", UnitPrice = 50 });
        }  
        [TestMethod]
        public void Get_All()
        {
            Mock<IProductDal> mock = new Mock<IProductDal>();
            ProductManager productManager = new ProductManager(new EfProductDal());

            Assert.AreEqual(81, productManager.GetAll().Count);
        }
    }
}
