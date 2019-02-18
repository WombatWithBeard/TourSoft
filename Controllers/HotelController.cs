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
    public class HotelController : Controller
    {
        private DataContext _context;

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] object o)
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
        public async Task<IActionResult> Get()
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