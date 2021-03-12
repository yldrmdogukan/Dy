using Dy.Core.DataAccess;
using Dy.Core.DataAccess.EntityFramework;
using Dy.Core.DataAccess.NHibernate;
using Dy.Northwind.Business.Abstract;
using Dy.Northwind.Business.Concrete.Managers;
using Dy.Northwind.DataAccess.Abstract;
using Dy.Northwind.DataAccess.Concrete.EntityFramework;
using Dy.Northwind.DataAccess.Concrete.NHibernate.Helpers;
using Ninject.Modules;
using System.Data.Entity;

namespace Dy.Northwind.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EfProductDal>().InSingletonScope();
            
            Bind<IUserService>().To<UserManager>();
            Bind<IUserDal>().To<EfUserDal>();
            
            Bind(typeof(IQueryableRepository<>)).To(typeof(EfQueryableRepository<>));

            Bind<DbContext>().To<NorthwindContext>();

            Bind<NHibernateHelper>().To<SqlServerHelper>();
        }
    }
}
