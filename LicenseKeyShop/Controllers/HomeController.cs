using LicenseKeyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LicenseKeyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {           
            if (HttpContext.Session.GetString("userAcc") != null)
            {
                string? useAcc = HttpContext.Session.GetString("userAcc");
                var userInf = Prn211He176850Context.INSTANCE.Users.Find(useAcc);
                ViewBag.userInf = userInf;
            }

            var random = new Random();
                
            var allProduct = Prn211He176850Context.INSTANCE.Products.ToList();
            var randomProduct = allProduct.OrderBy(r => random.Next()).Take(4).ToList();
            ViewBag.randomProduct = randomProduct;
            
            return View();
        }
    }
}