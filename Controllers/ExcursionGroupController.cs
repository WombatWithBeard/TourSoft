using System;
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
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ExcursionGroup excursionGroup)
        {
            try
            {
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
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid excrusionGroupId)
        {
            try
            {
                //TO DO: Проверка зависимостей? Функционал переопределения зависимостей
                var excursionGroup = _context.ExcursionGroups.FirstOrDefault(x => x.Id == excrusionGroupId);
                if (excursionGroup != null)
                {
                    _context.ExcursionGroups.Remove(excursionGroup);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("User was deleted successfully");
        }
    }
}