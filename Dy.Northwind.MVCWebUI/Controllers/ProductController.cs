using Dy.Northwind.Business.Abstract;
using Dy.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dy.Northwind.MVCWebUI.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };

            return View(model);
        }

        public string Add()
        {
            _productService.Add(new Product { CategoryId = 1, ProductName = "Selam", QuantityPerUnit = "1", UnitPrice = 120 });
            return "Added";
        }

        public string AddUpdate()
        {
            _productService.TransactionalOperation(new Product
            {
                CategoryId = 1,
                ProductName = "Laptop",
                QuantityPerUnit = "1",
                UnitPrice = 2
            },
            new Product
            {
                CategoryId = 1,
                ProductName = "Laptop",
                QuantityPerUnit = "1",
                UnitPrice = 10
            }
            );
            return "Done";
        }
    }
}
