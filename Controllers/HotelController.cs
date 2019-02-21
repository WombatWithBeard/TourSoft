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
    public class HotelController : Controller
    {
        private DataContext _context;

        [HttpPost]
        public IActionResult Add([FromBody] object o)
        {
            try
            {
                var data = o.ToString();
                var hotels = JsonConvert.DeserializeObject<List<Hotel>>(data);
                using (_context = new DataContext())
                {
                    foreach (var hotel in hotels)
                    {
                        _context.Hotels.Add(new Hotel(Guid.NewGuid(), hotel.Name, hotel.Address, hotel.PhoneNumber));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Success added hotel");
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            using (_context = new DataContext())
            {
                var result = JsonConvert.SerializeObject(_context.Hotels.Select(x => new
                    {
                        x.Name,
                        x.Address,
                        x.PhoneNumber,
                        x.Id,
                    })
                );
                return new ObjectResult(result);
            }
        }
    }
}