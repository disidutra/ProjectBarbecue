using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Barbecue.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEfBaseRepository<User> _context;
        public UserController(IEfBaseRepository<User> context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var result = await _context.GetById(id);
                if (result != null)
                {
                    return result;
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var result = await _context.GetAll();
                if (result.Any())
                {
                    return result.ToList();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]User item)
        {
            try
            {
                await _context.Add(item);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]User item)
        {
            try
            {
                var getItem = await _context.GetById(item.Id);
                if (getItem != null)
                {
                    await _context.Update(item);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            try
            {
                var item = await _context.GetById(id);
                if (item != null)
                {
                    await _context.Remove(item);
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}