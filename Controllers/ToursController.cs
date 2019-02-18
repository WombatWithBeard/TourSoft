using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Models;
using ToursSoft.Data.Models.Contexts;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    public class ToursController : Controller
    {
        private DataContext _context;

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] object o)
        {
            try
            {
                var data = o.ToString();
                var tours = JsonConvert.DeserializeObject<List<Tour>>(data);
                using (_context = new DataContext())
                {
                    foreach (var tour in tours)
                    {
                        _context.Tours.Add(new Tour(Guid.NewGuid(), tour.Name, tour.Capacity, tour.Description));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Success added tour");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (_context = new DataContext())
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
}