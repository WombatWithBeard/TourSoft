using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ToursController : Controller
    {
        private readonly DataContext _context;
        
        public ToursController(DataContext context)
        {
            _context = context;
        }       

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<Tour> tours)
        {
            try
            {
                foreach (var tour in tours)
                {
                    _context.Tours.Update(tour);
                }
                await  _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            return Ok("Info was updated successfully");
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] List<Tour> tours)
        {
            try
            {
                foreach (var tour in tours)
                {
                    await _context.Tours.AddAsync(tour);
                    //_context.Tours.Add(new Tour(Guid.NewGuid(), tour.Name, tour.Capacity, tour.Description));
                }
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("New tour was added successfully");
        }

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
            return new ObjectResult(result);
        }
    }
}