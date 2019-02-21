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
    public class ExcursionController : Controller
    {
        private DataContext _context;

        /// <summary>
        /// Get data about active excursion. If user is admin, return more information
        /// </summary>
        /// <param name="guidUser">Current user ID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromBody] Guid guidUser)
        {
            try
            {
                using (_context = new DataContext())
                {
                    var user = _context.Users.FirstOrDefault(x=>x.Id.Equals(guidUser));
                    if (user.IsAdmin)
                    {
                        var result = JsonConvert.SerializeObject(_context.Excursions.Where( a => a.Status)
                            .Select( x => new
                            {
                                x.DateTime,
                                x.Id,
                                x.Tour.Name,
                                x.Tour.Description,
                                x.Tour.Capacity,
                                x.ManagersGroup,
                            }));
                        return new ObjectResult(result);  
                    }
                    else
                    {
                        var result = JsonConvert.SerializeObject(_context.Excursions.Where( a => a.Status)
                            .Select( x => new
                            {
                                x.DateTime,
                                x.Id,
                                x.Tour.Name,
                                x.Tour.Description,
                                x.Tour.Capacity,
                            }));
                        return new ObjectResult(result);  
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        
        //TO DO: Check tour capacity
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guidExcursion"></param>
        /// <param name="guidUser"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] Guid guidExcursion, [FromBody] Guid guidUser, [FromBody] List<Person> persons)
        {
            try
            {
                foreach (var person in persons)
                {
                    _context.Excursions.Where(x => x.Id == guidExcursion).FirstOrDefault().ManagersGroup.Add(guidUser, person);  
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Success added persons to excursion");
        }
    }
}