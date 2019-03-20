using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.ViewModels;

namespace ToursSoft.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Account controller, with login, authorisation and logout functions
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public AccountController(DataContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Default get page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return BadRequest();
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

                        var a = HttpContext.User.Claims;
                        var c = "CfDJ8LOHfqnLf_dBhy_US_4gRicgOFTnh332Kfv9aOb47M9Jeoyc7B8MQB1CIZgf8CkhoS7-5sTqh9-WOPo4be8ZR41MvOj18buaLBJFKdOmU9YNPHsqxY6OfuP-nAC4CxfwGyh7la-rjlzzVAR9lTfumdJCokRswCW9BiDTNGS5VKTeFmfPhWeDaZ2tju98VNxT4Bv5MQUii5sGNMgfhrS_A8gsSZQW3dAz_FGg7yzRGFY2-9QAJvay8btTP6WHgJ5TEjOjFMcCQ_lRtbqOHG7o2D6Z2ED83BIZiPZq6aAFLoRa3g-CC0YDm7McTXreFZxGYbkgZa-NsVwcg59Wh3JKa_5ksmq3lBtQh64WlAVXacVmUCtmW89ZEFZbDen7B19uz0PYUchy5BhgFwTpSC4lQhw-12sVk_fyMcaBYWLCU_dY";
                        var b = "";
                        var e = c == b;
                        
                        return Ok(a);
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
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _context.Users.FirstOrDefault(x => x.Login == userName)?.Role),
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
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
            //return RedirectToAction("Login", "Account");
        }
    }
}