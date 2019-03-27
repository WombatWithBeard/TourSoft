using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// User controller with CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize(Roles = "admin")]
    public class RoleController: Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public RoleController(DataContext context, ILogger<RoleController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="rolesid"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Role> rolesid)
        {
            try
            {
                foreach (var roleid in rolesid)
                {
                    var role = _context.Roles.FirstOrDefault(x => x.Id == roleid.Id);
                    if (role != null)
                    {
                        _logger.LogInformation("Try to delete role: {0}", role.Id);
                        _context.Roles.Remove(role);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            
            _logger.LogInformation("Role was deleted by user: {0}", User.Identity.Name);
            return Ok("Role was deleted successfully");
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<Role> roles)
        {
            try
            {
                foreach (var role in roles)
                {
                    _logger.LogInformation("Try to update role: {0}", role.Id);
                    _context.Roles.Update(role);
                }
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }

            _logger.LogInformation("Role was updated by user: {0}", User.Identity.Name);
            return Ok("Info was updated successfully");
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<Role> roles)
        {
            try
            {
                foreach (var role in roles)
                {
                    if (_context.Roles.FirstOrDefault(x => x.Name == role.Name) != null)
                    {
                        return StatusCode(418, "Role name already in use");
                    }
                    _logger.LogWarning("Try to add new role");
                    await _context.Roles.AddAsync(role);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogWarning("New role was added by user: {0}", User.Identity.Name);
            return Ok("Role was added successfully");
        }

        /// <summary>
        /// Get info about users
        /// </summary>
        /// <returns></returns>
        //TODO: Check for admin and return more info? 
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("User {0} getting info about roles", User.Identity.Name);
            var result = JsonConvert.SerializeObject(_context.Roles.Select(x => new
                {
                    x.Name,
                    x.Description,
                    x.Id
                })
            );
            _logger.LogInformation("User {0} get role info", User.Identity.Name);
            return new ObjectResult(result);
        }
    }
}