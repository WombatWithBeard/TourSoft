using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ToursSoft.Auth;
using ToursSoft.Data.Contexts;
using ToursSoft.ViewModels;

namespace ToursSoft.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Account controller, with login, authorisation and logout functions
    /// </summary>
    [Route("/[controller]")]
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
                _logger.LogWarning("Invalid logging with login: {0}", model.Login);
                return;
            }
 
            var now = DateTime.UtcNow;
            // creating JWT-token
            var jwt = new JwtSecurityToken(
                    AuthOptions.Issuer,
                    AuthOptions.Audience,
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
            
            _logger.LogInformation("Success token granted for user: {0}", model.Login);
        }
 
        private ClaimsIdentity GetIdentity(LoginModel model)
        {
            var pass = Convert.ToBase64String(MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
            var user = _context.Users.FirstOrDefault(u => string.Equals(u.Login.ToLower(), model.Login.ToLower(), StringComparison.CurrentCultureIgnoreCase) && u.Password == pass);
            if (user == null) return null; // if we cant find user
            
            var claims = new List<Claim> {new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)};
            //TODO: Check this
            foreach (var userRole in _context.UserRoles.Include(ur => ur.Role)
                .Where(ur => ur.UserId == user.Id))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole.Role.Name.ToLower()));
            }
                    
            var claimsIdentity = new ClaimsIdentity(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        
        //TODO: check as unnecessary
        /// <summary>
        /// Logout user from server
        /// </summary>
        /// <returns>Redirect on Login view</returns>
        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}