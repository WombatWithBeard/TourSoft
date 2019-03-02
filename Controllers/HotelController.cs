﻿using System;
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

        /// <summary>
        /// Update info about hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Ok, or badrequest</returns>
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
        
        /// <summary>
        /// Add new hotels
        /// </summary>
        /// <param name="hotels"></param>
        /// <returns>Ok, or badrequest</returns>
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
        
        /// <summary>
        /// Get info about hotels
        /// </summary>
        /// <returns>Ok, or badrequest</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
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
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        /// <summary>
        /// Delete hotel by id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns>Ok, or badrequest</returns>
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