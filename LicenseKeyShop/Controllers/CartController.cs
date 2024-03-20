﻿using LicenseKeyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace License_Key_Shop_Web.Controllers
{
    public class CartController : Controller
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
                    var cartItemList = Prn211He176850Context.INSTANCE.CartItems
                        .Where(cartI => cartI.UserUsername == userInf.Username)
                        .Select(entity => new
                        {
                            ItemId = entity.ItemId,
                            Username = entity.UserUsername,
                            ProductInf = entity.ProductProduct,
                            Quantity = entity.Quantity,
                        });
                    var cartTotal = Prn211He176850Context.INSTANCE.Carts.Find(useAcc);
                    ViewBag.cartItemList = cartItemList;
                    ViewBag.cartTotal = cartTotal;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult AddToCart(IFormCollection f)
        {
            bool canAccess = CanAccessThisAdminPage();
            if (canAccess)
            {
                string? useAcc = HttpContext.Session.GetString("userAcc");
                int productId = Int32.Parse(f["productId"]);
                string oldPage = f["currentPage"];
                var cartItem = Prn211He176850Context.INSTANCE.CartItems
                    .Where(cartI => cartI.ProductProductId == productId && cartI.UserUsername.Equals(useAcc)).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                    Prn211He176850Context.INSTANCE.CartItems.Update(cartItem);
                }
                else
                {
                    var productToAdd = Prn211He176850Context.INSTANCE.Products.Find(productId);
                    if (productToAdd != null)
                    {
                        CartItem newItemInCart = new CartItem()
                        {
                            UserUsername = useAcc,
                            ProductProductId = productToAdd.ProductId,
                            Quantity = 1,
                        };
                        Prn211He176850Context.INSTANCE.CartItems.Add(newItemInCart);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                var userCart = Prn211He176850Context.INSTANCE.Carts.Find(useAcc);
                if (userCart != null)
                {
                    var addingProduct = Prn211He176850Context.INSTANCE.Products.Find(productId);
                    userCart.Total += addingProduct.Price;
                }
                Prn211He176850Context.INSTANCE.SaveChanges();
                return Redirect(oldPage);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Delete(int Id)
        {
            bool canAccess = CanAccessThisAdminPage();
            if (canAccess)
            {
                string? useAcc = HttpContext.Session.GetString("userAcc");
                var userInf = Prn211He176850Context.INSTANCE.Users.Find(useAcc);
                if (userInf != null)
                {
                    var productInCartOfCustomer = Prn211He176850Context.INSTANCE.CartItems
                                .Where(cartI => cartI.ProductProductId == Id && cartI.UserUsername.Equals(useAcc)).FirstOrDefault();
                    if (productInCartOfCustomer != null)
                    {
                        Prn211He176850Context.INSTANCE.CartItems.Remove(productInCartOfCustomer);
                        var cartTotalOfCus = Prn211He176850Context.INSTANCE.Carts.Find(useAcc);
                        cartTotalOfCus.Total -= productInCartOfCustomer.ProductProduct.Price * productInCartOfCustomer.Quantity;
                        Prn211He176850Context.INSTANCE.Carts.Update(cartTotalOfCus);
                        Prn211He176850Context.INSTANCE.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
       
}