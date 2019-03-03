using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController: Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
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
                //TO DO: Check for this login in the DB
                foreach (var user in users)
                {
                    await _context.Users.AddAsync(user);
                }
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("User was added successfully");
        }

        /// <summary>
        /// Get info about users
        /// </summary>
        /// <returns></returns>
        //TO DO: Check for admin and return more info? 
        [HttpGet]
        public IActionResult Get()
        {
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