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
    public class ExcursionController : Controller
    {
        private readonly DataContext _context;

        public ExcursionController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> ChangeStatus([FromBody] Guid guid)
        {
            try
            {
                var status = _context.Excursions.FirstOrDefault(x => x.Id == guid);
                if (status.Status)
                {
                    status.Status = false;
                }
                else if (status.Status == false)
                {
                    status.Status = true;
                }
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