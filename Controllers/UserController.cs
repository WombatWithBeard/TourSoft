﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models.Users;
using Microsoft.Extensions.Logging;

namespace ToursSoft.Controllers
{
    /// <summary>
    /// User controller with CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize(Roles = "admin")]
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
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] User userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == userId.Id);
                if (user != null)
                {
                    _logger.LogInformation("Try to delete user: {0}", user.Id);
                    _context.Users.Remove(user);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
            
            _logger.LogInformation("User was deleted by user: {0}", User.Identity.Name);
            return Ok("User was deleted successfully");
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                _logger.LogInformation("Try to update user: {0}", user.Id);
                _context.Users.Update(user);
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }

            _logger.LogInformation("User was updated by user: {0}", User.Identity.Name);
            return Ok("Info was updated successfully");
        }
        
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            try
            {
                if (_context.Users.FirstOrDefault(x => x.Login == user.Login) != null)
                {
                    return StatusCode(418, "login already in use");
                }
                _logger.LogWarning("Try to add new user");
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
            _logger.LogWarning("New User was added by user: {0}", User.Identity.Name);
            return Ok("User was added successfully");
        }

        /// <summary>
        /// Get info about users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {           
            _logger.LogInformation("User {username} getting info about users", User.Identity.Name);           
            var result = JsonConvert.SerializeObject(_context.Users
                .Select(x => new
                {
                    x.Name,
                    x.Company,
                    x.PhoneNumber,
                    x.Login,
                    RoleName = x.UserRoles.Select(ur => ur.Role.Name),
                    x.Id,
                })
            );
            _logger.LogInformation("User {0} get users info", User.Identity.Name);
            return new ObjectResult(result);
        }
    }
}