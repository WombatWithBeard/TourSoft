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
using ToursSoft.ViewModels;

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

        /// <summary>
        /// Login user on server
        /// </summary>
        /// <param name="model">login model - userLogin\userPassword</param>
        /// <returns>Returns default view, after success login</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            try
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
                    ModelState.AddModelError("", "Invalid login or password");
                    //return View(model);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            //TO DO:
            return Ok();
        }
        
        /// <summary>
        /// Authenticate method
        /// </summary>
        /// <param name="userName">userLogin</param>
        /// <returns>Cookies</returns>
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
        
        /// <summary>
        /// Logout user from server
        /// </summary>
        /// <returns>Redirect on Login view</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
            //return RedirectToAction("Login", "Account");
        }
    }
}