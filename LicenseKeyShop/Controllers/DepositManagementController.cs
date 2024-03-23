using LicenseKeyShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicenseKeyShop.Controllers
{
    public class DepositManagementController : Controller
    {
        public bool CanAccessThisManagementPage()
        {
            string? useAcc = HttpContext.Session.GetString("userAcc");
            if (useAcc != null)
            {
                var userInf = Prn211He176850Context.INSTANCE.Users.Find(useAcc);
                if (userInf != null)
                {
                    if (userInf.RoleRoleId != 1 && userInf.IsActive == true)
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
            bool canAccess = CanAccessThisManagementPage();
            if (canAccess)
            {
                var depositHistoryList = Prn211He176850Context.INSTANCE.DepositHistories.ToArray();
                ViewBag.depositHistoryList = depositHistoryList;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult Create(IFormCollection f)
        {
            string username = f["username"];
            var userInf = Prn211He176850Context.INSTANCE.Users.Find(username);
            if (userInf != null && userInf.IsActive == true && userInf.RoleRoleId == 1)
            {
                float amount = float.Parse(f["amount"]);
                string? useAcc = HttpContext.Session.GetString("userAcc");
                DateTime timeNow = DateTime.Now;

                var userBalInf = Prn211He176850Context.INSTANCE.UserBalances.Find(username);
                userBalInf.Amount += amount;
                Prn211He176850Context.INSTANCE.UserBalances.Update(userBalInf);

                
                DepositHistory depoHistoryInf = new DepositHistory()
                {
                    UserUsername = username,
                    Amount = amount,
                    ActionDate = timeNow,
                    ActionBy = useAcc
                };
                Prn211He176850Context.INSTANCE.DepositHistories.Add(depoHistoryInf);
                Prn211He176850Context.INSTANCE.SaveChanges();
            }
            return Redirect("/DepositManagement/Index");
        }
    }
}
