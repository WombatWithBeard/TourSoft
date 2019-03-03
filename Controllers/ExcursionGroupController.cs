using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ExcursionGroupController : Controller
    {
        private readonly DataContext _context;

        public ExcursionGroupController(DataContext context)
        {
            _context = context;
        }
                
        //TO DO: Check tour capacity
        //TO DO: Get
        
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
                Console.WriteLine();
                if (_context.Excursions.Where(x => x.Id == excursionGroup.ExcursionId && x.Status)
                    .Select(x => true).FirstOrDefault(x => x))
                {
                    _context.ExcursionGroups.Add(excursionGroup);
                    await  _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Uncorrect data");
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