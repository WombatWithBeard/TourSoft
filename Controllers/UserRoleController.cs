using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// User role controller with CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize(Roles = "admin")]
    public class UserRoleController: Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public UserRoleController(DataContext context, ILogger<UserRoleController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Delete UserRole by id
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<UserRole> userRoles)
        {
            try
            {
                foreach (var userRole in userRoles)
                {
                    var role = _context.UserRoles.FirstOrDefault(x => x.Id == userRole.Id);
                    if (role != null)
                    {
                        _logger.LogInformation("Try to delete user role: {0}", role.Id);
                        _context.UserRoles.Remove(role);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            
            _logger.LogInformation("UserRole was deleted by user: {0}", User.Identity.Name);
            return Ok("User role was deleted successfully");
        }

        /// <summary>
        /// Update UserRole info
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserRole userRole)
        {
            try
            {
                _logger.LogInformation("Try to update role: {0}", userRole.Id);
                _context.UserRoles.Update(userRole);
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
        /// Create new UserRole
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserRole userRole)
        {
            try
            {

                if (_context.UserRoles.FirstOrDefault(x => x.RoleId == userRole.RoleId && x.UserId == userRole.UserId) != null)
                {
                    return BadRequest("User already have this role");
                }
                _logger.LogWarning("Try to add new role");
                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogWarning("New user role was added by user: {0}", User.Identity.Name);
            return Ok("User role was added successfully");
        }

        /// <summary>
        /// Get info about UserRole
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("User {0} getting info about roles", User.Identity.Name);
            var result = JsonConvert.SerializeObject(_context.UserRoles
                .Include(ur => ur.Role)
                .Include(ur => ur.User)
                .Select(x => new
                {
                    RoleName = x.Role.Name,
                    x.Role.Description,
                    UserName = x.User.Name,
                    x.User.Login,
                    x.User.Company,
                    x.Id
                })
            );
            _logger.LogInformation("User {0} get user role info", User.Identity.Name);
            return new ObjectResult(result);
        }
    }
}