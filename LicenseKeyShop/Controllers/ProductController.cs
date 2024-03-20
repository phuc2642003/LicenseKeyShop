using LicenseKeyShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LicenseKeyShop.Controllers
{
    public class ProductController : Controller
    {
        public void loadUserInf()
        {
            string? useAcc = HttpContext.Session.GetString("userAcc");
            if (useAcc != null)
            {
                var userInf = Prn211He176850Context.INSTANCE.Users.Find(useAcc);
                ViewBag.userInf = userInf;
            }
        }
        public IActionResult Index()
        {
            loadUserInf();
            var prdList = Prn211He176850Context.INSTANCE.Products.Include(p=>p.CategoryCategory).ToList();
            ViewBag.prdList = prdList;
            return View();
        }

        public IActionResult Details(int Id)
        {
            loadUserInf();
            var prdDetails = Prn211He176850Context.INSTANCE.Products.Find(Id);
            ViewBag.prdDetails = prdDetails;
            return View();
        }
    }
}
