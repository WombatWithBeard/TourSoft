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
    /// Tour controller withs CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize(Roles = "admin")] //TODO:
    public class TourController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public TourController(DataContext context, ILogger<TourController> logger)
        {
            _logger = logger;
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
                foreach (var tourId in toursId)
                {
                    var tour = _context.Tours.FirstOrDefault(x => x.Id == tourId.Id);
                    if (tour != null)
                    {
                        _logger.LogInformation("Try to delete tour: {0}", tour.Id);
                        _context.Tours.Remove(tour);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogInformation("Tour was deleted by user: {0}", User.Identity.Name);
            return Ok("Tour was deleted successfully");
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
                    _logger.LogInformation("Try to update tour: {0}", tour.Id);
                    _context.Tours.Update(tour);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }

            _logger.LogInformation("Tour was updated by user: {0}", User.Identity.Name);
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
                    //TODO: check for name
                    _logger.LogInformation("Try to add new tour");
                    await _context.Tours.AddAsync(tour);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogInformation("Tour was added by user: {0}", User.Identity.Name);
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
            _logger.LogInformation("User {0} get tours info", User.Identity.Name);
            return new ObjectResult(result);
        }
        
        
    }
}