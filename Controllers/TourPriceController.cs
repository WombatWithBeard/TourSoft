using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Controllers
{
    /// <summary>
    /// Tour price controller with CRUD
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize]
    public class TourPriceController : Controller
    {
        private readonly DataContext _context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public TourPriceController(DataContext context)
        {
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
                if (User.IsInRole("Admin"))
                {
                    
                }
                else
                {
                    return Forbid("Access denied");
                }
                foreach (var tourPriceid in tourPricesid)
                {
                    var price = _context.TourPrices.FirstOrDefault(x => x.Id == tourPriceid.Id);
                    if (price != null)
                    {
                        _context.TourPrices.Remove(price);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Price was deleted successfully");
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="tourPrices"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<TourPrice> tourPrices)
        {
            try
            {
                foreach (var tourPrice in tourPrices)
                {
                    _context.TourPrices.Update(tourPrice);
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
        /// Create new user
        /// </summary>
        /// <param name="tourPrices"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<TourPrice> tourPrices)
        {
            try
            {
                foreach (var tourPrice in tourPrices)
                {
                    await _context.TourPrices.AddAsync(tourPrice);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Price was added successfully");
        }

        /// <summary>
        /// Get info about users
        /// </summary>
        /// <returns></returns>
        //TO DO: Check for admin and return more info? 
        [HttpGet]
        public IActionResult Get([FromBody] User user)
        {
            var result = JsonConvert.SerializeObject(_context.TourPrices.Where(u => u.User.Id == user.Id).Select(x => new
                {
                    TourName = x.Tour.Name,
                    x.Price,
                    x.Id,
                })
            );
            return new ObjectResult(result);
        }
    }
}