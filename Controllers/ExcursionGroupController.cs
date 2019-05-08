using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Controllers
{
    /// <summary>
    /// Excursion group controller with CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize]
    public class ExcursionGroupController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public ExcursionGroupController(DataContext context, ILogger<ExcursionGroupController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get info about excursion groups
        /// </summary>
        /// <param name="excursionid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(Guid excursionid)
        {
            try
            {
                object result;
                if (User.IsInRole("admin"))
                {
                    result = _context.ExcursionGroups
                        .Where(e => e.ExcursionId == excursionid)
                        .Select(x => new
                        {
                            x.User.Name,
                            x.Person,
                            HotelName = x.Person.Hotel.Name,
                            x.Id
                        });
                }
                else
                {
                    result = _context.ExcursionGroups
                        .Where(e => e.ExcursionId == excursionid && e.User.Login == User.Identity.Name)
                        .Select(x => new
                        {
                            x.User.Name,
                            x.Person,
                            x.Id
                        });
                }
                _logger.LogInformation("User {0} get excursion groups info", User.Identity.Name);
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update excursion groups info
        /// </summary>
        /// <param name="excursionGroupid"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ExcursionGroup excursionGroupid)
        {
            try
            {
                var excursionGroup = _context.ExcursionGroups.FirstOrDefault(x => x.Id == excursionGroupid.Id);
                if (excursionGroup != null)
                {
                    var currentCapacity = _context.GetExcursionGroupsCapacity(excursionGroup.ExcursionId);
                    //TODO: this shit dont work- excursion.Tour.Capacity
                    var a = _context.Tours.FirstOrDefault(x => x.Id == _context.Excursions.FirstOrDefault(e => e.Id == excursionGroup.ExcursionId).Id);
                    if (a != null && a.Capacity >= (currentCapacity + excursionGroup.GetCapacity(_context)))
                    {
                        _context.ExcursionGroups.Update(excursionGroup);
                        _logger.LogInformation("Try to update excursionGroup");
                
                        await  _context.SaveChangesAsync();
                    }
                    else
                    {
                        _logger.LogWarning("Not enough excursion space. Current workload is: {0} / {1}", 
                            currentCapacity, a.Capacity);
                        return BadRequest("Not enough excursion space");
                    }
                    
                    _logger.LogWarning("Try to update excursion group {0}", excursionGroup.Id);
                    _context.ExcursionGroups.Update(excursionGroup);
                }
                
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }

            _logger.LogWarning("ExcursionGroup was updated by user {0}", User.Identity.Name);
            return Ok("Excursion group was updated successfully");
        }

        /// <summary>
        /// Add info about excursion group
        /// </summary>
        /// <param name="excursionGroup">ExcursionGroup</param>
        /// <returns>Ok, or bad request</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ExcursionGroup excursionGroup)
        {
            try
            {
                var excursion = _context.Excursions.FirstOrDefault(x => x.Id == excursionGroup.ExcursionId);
                if (excursion != null && excursion.Status)
                {
                    var currentCapacity = _context.GetExcursionGroupsCapacity(excursionGroup.ExcursionId);
                    //TODO: this shit dont work- excursion.Tour.Capacity
                    var a = _context.Tours.FirstOrDefault(x => x.Id == excursion.TourId);
                   
                    if (a != null && a.Capacity >= (currentCapacity + excursionGroup.GetCapacity(_context)))
                    {
                        _context.ExcursionGroups.Add(excursionGroup);
                        _logger.LogInformation("Try to add new excursionGroup");
                    
                        await  _context.SaveChangesAsync();
                    }
                    else
                    {
                        _logger.LogWarning("Not enough excursion space. Current workload is: {0} / {1}", 
                            currentCapacity, a.Capacity);
                        return BadRequest("Not enough excursion space");
                    }
                }
                else
                {
                    _logger.LogError("Incorrect data for excursion groups from user: {0}", User.Identity.Name);
                    return BadRequest("Incorrect data");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
            _logger.LogInformation("ExcursionGroup was added by user: {0}", User.Identity.Name);
            return Ok("Excursion group was added to excursion successfully");
        }
        
        /// <summary>
        /// Delete excursion group by id
        /// </summary>
        /// <param name="excursionGroupId"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ExcursionGroup excursionGroupId)
        {
            try
            {
                var excursionGroup = _context.ExcursionGroups.FirstOrDefault(x => x.Id == excursionGroupId.Id);
                if (excursionGroup != null)
                {
                    _logger.LogInformation("Try to delete excursionGroup: {0}", excursionGroup.Id);
                    _context.ExcursionGroups.Remove(excursionGroup);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
            _logger.LogInformation("ExcursionGroup was deleted by user: {0}", User.Identity.Name);
            return Ok("Excursion group was deleted successfully");
        }
    }
}