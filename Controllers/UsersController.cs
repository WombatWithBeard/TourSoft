using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    public class UsersController
    {
        private DataContext _context;

//        [HttpPost]
//        public IActionResult Add([FromBody] object o)
//        {
//            try
//            {
//                var data = o.ToString();
//                var users = JsonConvert.DeserializeObject<List<User>>(data);
//                using (_context = new DataContext())
//                {
// 
//                }
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.ToString());
//            }
//            return Ok("Success added tour");
//        }

        [HttpGet]
        public IActionResult Get()
        {
            using (_context = new DataContext())
            {
                var result = JsonConvert.SerializeObject(_context.Tours
                    .Select(x => new
                    {
                        x.Name,
                        x.Capacity,
                        x.Description,
                        x.Id,
                    })
                );
                return new ObjectResult(result);
            }
        }
    }
}