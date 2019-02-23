using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Tree;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    public class UsersController: Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Update([FromBody] Guid userid, User user)
        {
            try
            {
                _context.Users.Where(x => x.Id.Equals(userid));
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            return Ok("Info was updated successfully");
        }
        
        [HttpPost]
        public IActionResult Add([FromBody] List<User> users)
        {
            try
            {
                foreach (var user in users)
                {
                    //TO DO: Test
                     _context.Users.Add(user);
                }
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
            var result = JsonConvert.SerializeObject(_context.Users
                .Select(x => new
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