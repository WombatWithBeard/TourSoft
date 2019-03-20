using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Excursion group controller with CRUD
    /// </summary>
    [Route("api/[controller]")]
    //[Authorize] //TODO:
    public class ExcursionGroupController : Controller
    {
        private readonly DataContext _context;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public ExcursionGroupController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get info about excursion groups
        /// </summary>
        /// <param name="excursion"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] Excursion excursion)
        {
            try
            {
                object result;
                if (User.IsInRole("Admin"))
                {
                    result = _context.ExcursionGroups.Where(e => e.ExcursionId == excursion.Id)
                        .Select(x => new
                        {
                            x.User.Name,
                            x.Person,
                            x.Id
                        });
                }
                else
                {
                    result = _context.ExcursionGroups.Where(e => e.ExcursionId == excursion.Id && 
                                                                     e.User.Login == User.Identity.Name)
                        .Select(x => new
                        {
                            x.User.Name,
                            x.Person,
                            x.Id
                        });
                }
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Update excursion groups info
        /// </summary>
        /// <param name="excursionGroupsid"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<ExcursionGroup> excursionGroupsid)
        {
            try
            {
                foreach (var excursionGroupid in excursionGroupsid)
                {
                    var excursionGroup = _context.ExcursionGroups.FirstOrDefault(x => x.Id == excursionGroupid.Id);
                    if (excursionGroup != null)
                    {
                        _context.ExcursionGroups.Update(excursionGroup);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            return Ok("Excursion group was deleted successfully");
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
                if (_context.Excursions.Where(x => x.Id == excursionGroup.ExcursionId && x.Status)
                    .Select(x => true).FirstOrDefault(x => x))
                {
                    //TODO: check for capacity
                    _context.ExcursionGroups.Add(excursionGroup);
                    await  _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Incorrect data");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Excursion group was added to excursion successfully");
        }
        
        /// <summary>
        /// Delete excursion group by id
        /// </summary>
        /// <param name="excursionGroupsId"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<ExcursionGroup> excursionGroupsId)
        {
            try
            {
                foreach (var excursionGroupId in excursionGroupsId)
                {
                    var excursionGroup = _context.ExcursionGroups.FirstOrDefault(x => x.Id == excursionGroupId.Id);
                    if (excursionGroup != null)
                    {
                        _context.ExcursionGroups.Remove(excursionGroup);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Excursion group was deleted successfully");
        }
    }
}