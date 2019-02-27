using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

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
        
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ExcursionGroup excursionGroup)
        {
            try
            {
                if (_context.Excursions.Where(x => x.Id == excursionGroup.ExcursionId && x.Status)
                    .Select(x => true).FirstOrDefault(x => x))
                {
                    _context.ExcursionGroups.Add(excursionGroup);
                
                    //TO DO: Delete
                    
//                    var personId = Guid.NewGuid();
//                    excursionGroup.Person.Id = personId;
//                    _context.Persons.Add(excursionGroup.Person);
//                    
//                    var mg = new ExcursionGroup(Guid.NewGuid(), personId, excursionGroup.UserId, excursionGroup.ExcursionId);
//                    _context.ExcursionGroups.Add(mg);

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
    }
}