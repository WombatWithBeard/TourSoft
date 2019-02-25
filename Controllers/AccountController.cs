using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var pass = Convert.ToBase64String(MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == pass);
                if (user != null)
                {
                    await Authenticate(model.Login);
 
                    //return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            //TO DO:
            //return View(model);
            return Ok();
        }
        
        private async Task Authenticate(string userName)
        {
            // create claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            };
            
            // create object ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            // setup authenticated cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
            //return RedirectToAction("Login", "Account");
        }
    }
}