using Dy.Core.CrossCuttingConcerns.Security.Web;
using Dy.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dy.Northwind.MVCWebUI.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(string userName, string password)
        {
            var user = _userService.GetByUserNameAndPassword(userName, password);
            if (user != null)
            {
                AuthenticationHelper.CreateAuthCookie(
                    Guid.NewGuid(),
                    user.UserName,
                    user.EMail,
                    DateTime.Now.AddDays(15),
                    _userService.GetUserRoles(user).Select(x=>x.RoleName).ToArray(),
                    false,
                    user.FirstName,
                    user.LastName);

                return "User is authenticated";
            }

            return "User is not authenticated";
        }
    }
}