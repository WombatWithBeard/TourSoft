﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
//                        var result = JsonConvert.SerializeObject(_context.Excursions.Where( a => a.Status)
//                            .Select( x => new
//                            {
//                                x.DateTime,
//                                x.Id,
//                                x.Tour.Name,
//                                x.Tour.Description,
//                                x.Tour.Capacity,
//                                x.ManagersGroup,
//                            }));
//                        return new ObjectResult(result);
                        return Ok();
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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<Excursion> excursions)
        {
            try
            {
                foreach (var excursion in excursions)
                {
                    await _context.Excursions.AddAsync(excursion); 
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("");
        }
        
        //TO DO: Check tour capacity
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guidExcursion"></param>
        /// <param name="guidUser"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Guid guidExcursion, [FromBody] Guid guidUser, [FromBody] Person person)
        {
            try
            {
                if (_context.Excursions.Where(x => x.Id == guidExcursion && x.GetCapacity(person) && x.Status)
                    .Select(x => true).FirstOrDefault(x => x))
                {
                    foreach (var contextExcursion in _context.Excursions.Where(x => x.Id == guidExcursion))
                    {
                        contextExcursion.ManagersGroup.Add(new ManagersGroup {Person = person, ManagerId = guidUser});
                    } 
                }
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Success added persons to excursion");
        }
    }
}