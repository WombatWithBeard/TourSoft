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
    /// Excursion controller with CRUD
    /// </summary>
    [Route("/[controller]")]
    [Authorize]
    public class ExcursionController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public ExcursionController(DataContext context, ILogger<ExcursionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Delete excursion by id
        /// </summary>
        /// <param name="excursionsId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Excursion> excursionsId)
        {
            try
            {
                if (User.IsInRole("admin"))
                {
                    foreach (var excursionid in excursionsId)
                    {
                        var excursion = _context.Excursions.FirstOrDefault(x => x.Id == excursionid.Id);
                        if (excursion != null)
                        {
                            _logger.LogInformation("Try to delete excursion: {0}", excursion.Id);
                            _context.Excursions.Remove(excursion);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning("Access denied for user {0}", User.Identity.Name);
                    return BadRequest("Access denied");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
            _logger.LogInformation("Excursions was deleted by user: {0}", User.Identity.Name);
            return Ok("Excursion was deleted successfully");
        }

        /// <summary>
        /// Changing status of the excursion
        /// </summary>
        /// <param name="excursion"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPut("ChangeStatus")]
        public IActionResult ChangeStatus([FromBody] Excursion excursion)
        {
            //TODO: make scheduler for status, i think
            try
            {
                if (User.IsInRole("admin"))
                {
                    
                    var status = _context.Excursions.FirstOrDefault(x => x.Id == excursion.Id);
                    if (status != null && status.Status)
                    {
                        status.Status = false;
                    }
                    else if (status != null && status.Status == false)
                    {
                        status.Status = true;
                    }
                    
                    _context.SaveChanges();
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
            _logger.LogInformation("Status for excursion: {0} was changed by user: {1}", excursion.Id, User.Identity.Name);
            return Ok("Excursion status was changed");
        }

        /// <summary>
        /// Get data about active excursion. If user is admin, return more information
        /// </summary>
        /// <returns>Ok, or bad request</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                object result;               
                //TODO:bad if
                if (User.IsInRole("admin"))
                {
                    result = JsonConvert.SerializeObject(_context.Excursions
                        .Select(x => new
                        {
                            x.DateTime,
                            x.Status,
                            x.Tour.Name,
                            x.Tour.Capacity,
                            currentcapacity = _context.GetExcursionGroupsCapacity(x.Id),
                            x.Id
                        }));
                }
                else
                {
                    result = JsonConvert.SerializeObject(_context.Excursions.Where(s => s.Status)
                        .Select(x => new
                        {
                            x.DateTime,
                            x.Tour.Name,
                            x.Tour.Capacity,
                            currentcapacity = _context.GetExcursionGroupsCapacity(x.Id),
                            x.Id
                        }));
                }
                _logger.LogInformation("User: {0} get, excursions info", User.Identity.Name);
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Update info about excursion
        /// </summary>
        /// <param name="excursions"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] List<Excursion> excursions)
        {
            try
            {
                if (User.IsInRole("admin"))
                {
                    foreach (var excursion in excursions)
                    {
                        _logger.LogInformation("Try to update excursion: {0}", excursion.Id);
                        _context.Excursions.Update(excursion);
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
            
            //TODO:
            
            _logger.LogInformation("Excursions was update by user: {0}", User.Identity.Name);
            return Ok();
        }

        /// <summary>
        /// Creating new excursion
        /// </summary>
        /// <param name="excursions">excursion info</param>
        /// <returns>Ok, or bad request</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<Excursion> excursions)
        {
            try
            {
                if (User.IsInRole("admin"))
                {
                    foreach (var excursion in excursions)
                    {
                        _logger.LogInformation("Try to add new excursion");
                        await _context.Excursions.AddAsync(excursion);
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
            _logger.LogInformation("Excursions was added by user: {0}", User.Identity.Name);
            return Ok("Excursion was added successfully");
        }
    }
}