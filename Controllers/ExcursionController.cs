using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;
using ToursSoft.Data.Models.Service;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ExcursionController : Controller
    {
        private DataContext _context;

        public ExcursionController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get data about active excursion. If user is admin, return more information
        /// </summary>
        /// <param name="guidUser">Current user ID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()//([FromBody] Guid guidUser)
        {
            try
            {
                var result = JsonConvert.SerializeObject(_context.Excursions.Where(a => a.Status)
                    .Select(x => new
                    {
                        //TO DO: вытащить информацию по людям, через отдельную модель
                        x.DateTime,
                        x.Status,
                        x.Tour.Name,
                        x.Tour.Capacity,
                        x.Id
                    }));
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] List<Excursion> excursions)
        {
            try
            {
                foreach (var excursion in excursions)
                {
                    await _context.Excursions.AddAsync(excursion);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Excursion was added sucecssfully");
        }
        
        //TO DO: Check tour capacity

        
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ExcursionAddRequest excursionAddRequest)
        {
            //TO DO
            try
            {
                if (_context.Excursions.Where(x => x.Id == excursionAddRequest.ExcursionId && x.Status)
                    .Select(x => true).FirstOrDefault(x => x))
                {
                    var personId = Guid.NewGuid();
                    excursionAddRequest.Person.Id = personId;
                    _context.Persons.Add(excursionAddRequest.Person);
                    
                    var mg = new ManagersGroup(Guid.NewGuid(), personId, excursionAddRequest.UserId, excursionAddRequest.ExcursionId);
                    _context.ManagersGroups.Add(mg);

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
            return Ok("Person was added to excursion successfully");
        }
    }
}