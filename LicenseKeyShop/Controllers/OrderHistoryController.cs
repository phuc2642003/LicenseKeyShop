using LicenseKeyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LicenseKeyShop.Controllers
{
    public class OrderHistoryController : Controller
    {
        public bool CanAccessThisPage()
        {
            string? useAcc = HttpContext.Session.GetString("userAcc");
            if (useAcc != null)
            {
                var userInf = Prn211He176850Context.INSTANCE.Users.Find(useAcc);
                if (userInf != null)
                {
                    if (userInf.RoleRoleId == 1 && userInf.IsActive == true)
                    {
                        var roleList = Prn211He176850Context.INSTANCE.Roles.ToArray();
                        ViewBag.userInf = userInf;
                        ViewBag.roleList = roleList;
                        return true;
                    }
                }
            }
            if (HttpContext.Session.Keys.Contains("userAcc"))
            {
                HttpContext.Session.Remove("userAcc");
            }
            return false;
        }
        public IActionResult Index()
        {
            bool canAccess = CanAccessThisPage();
            if (canAccess)
            {
                string? useAcc = HttpContext.Session.GetString("userAcc");
                var userInf = Prn211He176850Context.INSTANCE.Users.Find(useAcc);
                if (userInf != null)
                {
                    var orderHis = Prn211He176850Context.INSTANCE.OrderHistories
                        .Where(order => order.UserUsername == userInf.Username)
                        .Select(entity => new
                        {
                            OrderId = entity.OrderId,
                            UserUsername = entity.UserUsername,
                            OrderDetailHe173252 = entity.OrderDetails
                        })
                        .OrderByDescending(ord => ord.OrderId);
                    ViewBag.orderHis = orderHis;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
