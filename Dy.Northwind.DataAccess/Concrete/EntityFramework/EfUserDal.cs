using Dy.Core.DataAccess.EntityFramework;
using Dy.Northwind.DataAccess.Abstract;
using Dy.Northwind.Entities.ComplexTypes;
using Dy.Northwind.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Dy.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<UserRoleItem> GetUserRoles(User user)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from ur in context.UserRoles
                             join r in context.Roles
                             on ur.UserId equals user.Id
                             where ur.UserId == user.Id
                             select new UserRoleItem { RoleName = r.Name };
                return result.ToList();
            }
        }
    }
}
