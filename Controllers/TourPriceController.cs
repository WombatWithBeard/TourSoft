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
    /// <summary>
    /// Tour price controller with CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize]
    public class TourPriceController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public TourPriceController(DataContext context, ILogger<TourPriceController> logger)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Delete price by id
        /// </summary>
        /// <param name="tourPricesid"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<TourPrice> tourPricesid)
        {
            try
            {
                if (User.IsInRole("admin"))
                {
                    foreach (var tourPriceid in tourPricesid)
                    {
                        var price = _context.TourPrices.FirstOrDefault(x => x.Id == tourPriceid.Id);
                        if (price != null)
                        {
                            _logger.LogInformation("Try to delete tourprice: {0}", price.Id);
                            _context.TourPrices.Remove(price);
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("Access denied for user {0}", User.Identity.Name);
                    return Forbid("Access denied");
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
            
            _logger.LogInformation("TourPrice was deleted by user: {0}", User.Identity.Name);
            return Ok("Price was deleted successfully");
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="tourPrice"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TourPrice tourPrice)
        {
            try
            {
                _logger.LogInformation("Try to update tourPrice: {0}", tourPrice.Id);
                _context.TourPrices.Update(tourPrice);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }

            _logger.LogInformation("TourPrice was updated by user: {0}", User.Identity.Name);
            return Ok("Info was updated successfully");
        }
        
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="tourPrice"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TourPrice tourPrice)
        {
            try
            {

                _logger.LogInformation("Try to add new tourPrice");
                await _context.TourPrices.AddAsync(tourPrice);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
            _logger.LogInformation("TourPrice was added by user: {0}", User.Identity.Name);
            return Ok("Price was added successfully");
        }

        /// <summary>
        /// Get info about users
        /// </summary>
        /// <returns></returns>
        //TODO: Check for admin and return more info? 
        [HttpGet]
        public IActionResult Get()
        {
            object result;
            if (User.IsInRole("admin"))
            {
                result = JsonConvert.SerializeObject(_context.TourPrices.Select(x => new
                    {
                        TourName = x.Tour.Name,
                        x.User.Name,
                        x.Price,
                        x.Id,
                    })
                );
            }
            else
            {
                result = JsonConvert.SerializeObject(_context.TourPrices.Where(u => u.User.Login == User.Identity.Name).Select(x => new
                    {
                        TourName = x.Tour.Name,
                        x.Price,
                        x.Id,
                    })
                );
            }
            _logger.LogInformation("User {0} get tourprices info", User.Identity.Name);
            return new ObjectResult(result);
        }
    }
}