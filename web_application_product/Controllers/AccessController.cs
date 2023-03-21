﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using web_application_product.Models;

namespace web_application_product.Controllers
{
    public class AccessController : Controller
    {
       
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Products");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin,string ReturnUrl)
        {

            if (modelLogin.Email == "Govind@example.com" &&
                modelLogin.PassWord == "123"
                )
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties","Example Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Products");
            }



            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
        }
}
