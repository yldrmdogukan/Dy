using Dy.Core.Aspects.PostSharp.AuthorizationAspects;
using Dy.Core.Aspects.PostSharp.CacheAspects;
using Dy.Core.Aspects.PostSharp.LogAspects;
using Dy.Core.Aspects.PostSharp.PerformanceAspects;
using Dy.Core.Aspects.PostSharp.TransactionAspects;
using Dy.Core.Aspects.PostSharp.ValidationAspects;
using Dy.Core.CrossCuttingConcerns.Caching.Microsoft;
using Dy.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Dy.Core.CrossCuttingConcerns.Validation.FluentValidation;
using Dy.Northwind.Business.Abstract;
using Dy.Northwind.Business.ValidationRules;
using Dy.Northwind.DataAccess.Abstract;
using Dy.Northwind.Entities.Concrete;
using System.Collections.Generic;
using System.Threading;

namespace Dy.Northwind.Business.Concrete.Managers
{
    
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Add(Product product)
        {
            ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Add(product);
        }

        [PerformanceCounterAspect(1)]
        [SecuredOperation(Roles="Admin,Editor,Student")]
        public List<Product> GetAll()
        {
            Thread.Sleep(500);
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(x => x.ProductId == id);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            //Business Codes
            _productDal.Update(product2);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        {
            ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Update(product);
        }
    }
}
