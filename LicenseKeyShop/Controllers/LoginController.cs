using LicenseKeyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using LicenseKeyShop.Ultis;
using Microsoft.AspNetCore.Http;

namespace LicenseKeyShop.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.username = "";
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormCollection f)
        {
            String username = f["username"];
            String password = f["password"];
            var urs = Prn211He176850Context.INSTANCE.Users.Find(username);
            if (urs == null)
            {
                ViewBag.loginUsernameErr = "Username does not exists!";
            }
            else
            {
                if (urs.Password == Hasing_SHA256.SHA256Encrypt(password))
                {
                    HttpContext.Session.SetString("userAcc", urs.Username);
                    if (urs.RoleRoleId == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ViewBag.loginPasswordErr = "Incorrect password!";
                }

            }
            ViewBag.username = username;
            return View();
        }
    }
}
