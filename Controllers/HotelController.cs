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
    /// Hotel controller with CRUD
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize]
    public class HotelController : Controller
    {
        private readonly DataContext _context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public HotelController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Update info about hotel
        /// </summary>
        /// <param name="hotels"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<Hotel> hotels)
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
                foreach (var hotel in hotels)
                {
                    _context.Hotels.Update(hotel);  
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
        /// Add new hotels
        /// </summary>
        /// <param name="hotels"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<Hotel> hotels)
        {
            try
            {
                foreach (var hotel in hotels)
                {
                    //TO DO:
                    _context.Hotels.Add(hotel);
                }
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("New hotel was added successfully");
        }
        
        /// <summary>
        /// Get info about hotels
        /// </summary>
        /// <returns>Ok, or bad request</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = JsonConvert.SerializeObject(_context.Hotels.Select(x => new
                    {
                        x.Name,
                        x.Address,
                        x.PhoneNumber,
                        x.Id,
                    })
                );
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Delete hotel by id
        /// </summary>
        /// <param name="hotelsId"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Hotel> hotelsId)
        {
            try
            {
                foreach (var hotelId in hotelsId)
                {
                    //TO DO: Проверка зависимостей? Функционал переопределения зависимостей
                    var hotel = _context.Hotels.FirstOrDefault(x => x.Id == hotelId.Id);
                    if (hotel != null)
                    {
                        _context.Hotels.Remove(hotel);
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
    }
}