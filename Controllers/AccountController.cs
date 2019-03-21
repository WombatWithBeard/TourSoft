using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ToursSoft.Auth;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models.Users;
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
        private readonly ILogger  _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public AccountController(DataContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task Login([FromBody]LoginModel model)
        {
            var identity = GetIdentity(model);
            if (identity == null)
            {
                Response.StatusCode = 401;
                await Response.WriteAsync("Invalid username or password.");
                _logger.LogWarning("Invalid logging with login: " + model.Login);
                return;
            }
 
            var now = DateTime.UtcNow;
            // creating JWT-token
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
             
            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name
            };
 
            // serializing response
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
            
            _logger.LogInformation("Success token granted");
        }
 
        private ClaimsIdentity GetIdentity(LoginModel model)
        {
            var pass = Convert.ToBase64String(MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
            var user = _context.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == pass);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
 
            // if we cant find user
            return null;
        }
        
        //TODO: check as unnecessary
        /// <summary>
        /// Logout user from server
        /// </summary>
        /// <returns>Redirect on Login view</returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}