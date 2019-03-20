using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    /// <summary>
    /// Tour controller withs CRUD
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize]
    public class TourController : Controller
    {
        private readonly DataContext _context;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public TourController(DataContext context)
        {
            _context = context;
        }       

        /// <summary>
        /// Delete tour by id
        /// </summary>
        /// <param name="toursId"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Tour> toursId)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    
                }
                else
                {
                    return Forbid("Access denied");
                }
                foreach (var tourId in toursId)
                {
                    var tour = _context.Tours.FirstOrDefault(x => x.Id == tourId.Id);
                    if (tour != null)
                    {
                        _context.Tours.Remove(tour);
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
        /// Update info about tours
        /// </summary>
        /// <param name="tours"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<Tour> tours)
        {
            try
            {
                foreach (var tour in tours)
                {
                    _context.Tours.Update(tour);
                }
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            return Ok("Info was updated successfully");
        }
        
        /// <summary>
        /// Create new tours
        /// </summary>
        /// <param name="tours"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<Tour> tours)
        {
            try
            {
                foreach (var tour in tours)
                {
                    await _context.Tours.AddAsync(tour);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("New tour was added successfully");
        }

        /// <summary>
        /// Get info about all tours
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
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