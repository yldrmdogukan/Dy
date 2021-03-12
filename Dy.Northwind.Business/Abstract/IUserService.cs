using Dy.Northwind.Entities.ComplexTypes;
using Dy.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dy.Northwind.Business.Abstract
{
    public interface IUserService
    {
        User GetByUserNameAndPassword(string userName,string password);
        List<UserRoleItem> GetUserRoles(User user);
    }
}
