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
    public class ExcursionController : Controller
    {
        private readonly DataContext _context;

        public ExcursionController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Delete excursion by id
        /// </summary>
        /// <param name="excursionsId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<Excursion> excursionsId)
        {
            try
            {
                foreach (var excursionid in excursionsId)
                {
                    var excursion = _context.Excursions.FirstOrDefault(x => x.Id == excursionid.Id);
                    if (excursion != null)
                    {
                        _context.Excursions.Remove(excursion);  
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Excursion was deleted successfully");
        }

        /// <summary>
        /// Changing status of the excursion
        /// </summary>
        /// <param name="excursion"></param>
        /// <returns>Ok, or bad request</returns>
        [HttpPut("ChangeStatus")]
        public IActionResult ChangeStatus([FromBody] Excursion excursion)
        {
            try
            {
                var status = _context.Excursions.FirstOrDefault(x => x.Id == excursion.Id);
                if (status != null && status.Status)
                {
                    status.Status = false;
                }
                else if (status != null && status.Status == false)
                {
                    status.Status = true;
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok("Excursion status was changed");
        }

        /// <summary>
        /// Get data about active excursion. If user is admin, return more information
        /// </summary>
        /// <returns>Ok, or bad request</returns>
        [HttpGet]
        public IActionResult Get()//([FromBody] Guid guidUser)
        {
            try
            {
                //TO DO: make Admin info
                var result = JsonConvert.SerializeObject(_context.Excursions//.Where(a => a.Status)
                    .Select(x => new
                    {
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

        /// <summary>
        /// Update info about excursion
        /// </summary>
        /// <param name="excursions"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] List<Excursion> excursions)
        {
            try
            {
                foreach (var excursion in excursions)
                {
                    _context.Excursions.Update(excursion);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok();
        }

        /// <summary>
        /// Creating new excursion
        /// </summary>
        /// <param name="excursions">excursion info</param>
        /// <returns>Ok, or bad request</returns>
        [HttpPost]
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
            return Ok("Excursion was added successfully");
        }
    }
}