using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ToursSoft.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// User controller with CRUD
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")] //TODO: check authorize role
    public class UserController: Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public UserController(DataContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="usersId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<User> usersId)
        {
            try
            {
                foreach (var userid in usersId)
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == userid.Id);
                    if (user != null)
                    {
                        _context.Users.Remove(user);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            return Ok("User was deleted successfully");
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<User> users)
        {
            try
            {
                foreach (var user in users)
                {
                    _context.Users.Update(user);
                }
                await  _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }

            return Ok("Info was updated successfully");
        }
        
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<User> users)
        {
            try
            {
                foreach (var user in users)
                {
                    if (_context.Users.FirstOrDefault(x => x.Login == user.Login) != null)
                    {
                        return StatusCode(418, "login already in use");
                    }
                    _logger.LogWarning("Try to add new user");
                    await _context.Users.AddAsync(user);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogWarning("New User was add by user: {0}", User.Identity.Name);
            return Ok("User was added successfully");
        }

        /// <summary>
        /// Get info about users
        /// </summary>
        /// <returns></returns>
        //TODO: Check for admin and return more info? 
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("User {username} getting info about users", User.Identity.Name);
            var result = JsonConvert.SerializeObject(_context.Users.Select(x => new
                {
                    x.Name,
                    x.Company,
                    x.PhoneNumber,
                    x.Id,
                })
            );
            return new ObjectResult(result);
        }
    }
}