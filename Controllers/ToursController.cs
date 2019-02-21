using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToursSoft.Data.Contexts;
using ToursSoft.Data.Models;

namespace ToursSoft.Controllers
{
    [Route("api/[controller]")]
    public class ToursController : Controller
    {
        private DataContext _context;
        
        public ToursController(DataContext context)
        {
            _context = context;
        }       

        [HttpPost]
        public IActionResult Add([FromBody] List<Tour> tours)
        {
            try
            {
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
                Console.WriteLine(e.ToString());
                return BadRequest(e.ToString());
            }
            return Ok("Success added tour");
        }

        [HttpGet]
        public IActionResult Get()
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