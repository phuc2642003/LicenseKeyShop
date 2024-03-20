using LicenseKeyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LicenseKeyShop.Controllers
{
    public class DepositController : Controller
    {
        public bool CanAccessThisAdminPage()
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
            bool canAccess = CanAccessThisAdminPage();
            if (canAccess)
            {
                string? useAcc = HttpContext.Session.GetString("userAcc");
                var userInf = Prn211He176850Context.INSTANCE.Users.Find(useAcc);
                if (userInf != null)
                {
                    var userBalance = Prn211He176850Context.INSTANCE.UserBalances.Find(useAcc);
                    ViewBag.userBalance = userBalance;
                    var depositHistoryList = Prn211He176850Context.INSTANCE.DepositHistories
                        .Where(deposit => deposit.UserUsername.Equals(useAcc))
                        .Select(entity => new
                        {
                            DepositId = entity.DepositId,
                            UserUsername = entity.UserUsername,
                            Amount = entity.Amount,
                            ActionDate = entity.ActionDate,
                            ActionBy = entity.ActionBy,
                        });
                    ViewBag.depositHistoryList = depositHistoryList;
                }
                ViewBag.userInf = userInf;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
