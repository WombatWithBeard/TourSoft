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
    public class HotelController : Controller
    {
        private readonly DataContext _context;

        public HotelController(DataContext context)
        {
            _context = context;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Hotel hotel)
        {
            try
            {
                _context.Hotels.Update(hotel);
                await  _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

            return Ok("Info was updated successfully");
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<Hotel> hotels)
        {
            try
            {
                foreach (var hotel in hotels)
                {
                    //TO DO:
                    _context.Hotels.Add(hotel);
                }
                await  _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("New hotel was added successfully");
        }
        
        [HttpGet]
        public IActionResult Get()
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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid hotelId)
        {
            try
            {
                //TO DO: Проверка зависимостей? Функционал переопределения зависимостей
                var hotel = _context.Hotels.FirstOrDefault(x => x.Id == hotelId);
                if (hotel != null)
                {
                    _context.Hotels.Remove(hotel);
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