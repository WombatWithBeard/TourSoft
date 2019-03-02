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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid userId)
        {
            try
            {
                //TO DO: Проверка зависимостей? Функционал переопределения зависимостей
                var user = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("User was deleted successfully");
        }

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
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<User> users)
        {
            try
            {
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
                    x.Password,
                })
            );
            return new ObjectResult(result);
        }
    }
}