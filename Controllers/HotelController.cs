using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Hotel controller with CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize(Roles = "admin")]
    public class HotelController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public HotelController(DataContext context, ILogger<HotelController> logger)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Update info about hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Hotel hotel)
        {
            try
            {
                if (User.IsInRole("admin"))
                {
                    _logger.LogInformation("Try to update hotel: {0}", hotel.Id);
                    _context.Hotels.Update(hotel);  
                    await  _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning("Access denied for user: {0}", User.Identity.Name);
                    return Forbid("Access denied");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }

            _logger.LogInformation("Hotel was updated by user: {0}", User.Identity.Name);
            return Ok("Info was updated successfully");
        }
        
        /// <summary>
        /// Add new hotels
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Hotel hotel)
        {
            try
            {
                if (_context.Hotels.Any(x => x.Name == hotel.Name))
                {
                    return BadRequest("This hotel name already exists");
                }
                _logger.LogInformation("Try to add new hotel");
                _context.Hotels.Add(hotel);
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogInformation("Hotel was added by user: {0}", User.Identity.Name);
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
                _logger.LogInformation("User {0} get hotels info", User.Identity.Name);
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
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
                if (User.IsInRole("admin"))
                {
                    foreach (var hotelId in hotelsId)
                    {
                        var hotel = _context.Hotels.FirstOrDefault(x => x.Id == hotelId.Id);
                        if (hotel != null)
                        {
                            _logger.LogInformation("Try to delete hotel: {0}", hotel.Id);
                            _context.Hotels.Remove(hotel);
                        }
                    }
                    await _context.SaveChangesAsync(); 
                }
                else
                {
                    _logger.LogWarning("Access denied for user: {0}", User.Identity.Name);
                    return Forbid("Access denied");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogInformation("Hotel was deleted by user: {0}", User.Identity.Name);
            return Ok("User was deleted successfully");
        }
    }
}