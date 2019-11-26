using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;
using Barbecue.Infrastructure.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Barbecue.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventUserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly EventUserRepository _context;
        private const string LocalLog = "[WebAPI][EventUserController]";
        public EventUserController(ILogger<EventUserController> logger, EventUserRepository context)
        {
            _logger = logger;
            _context = context;
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<EventUser>> Get(object obj)
        // {
        //     try
        //     {
        //         var result = await _context.GetByIdCompositeKey(obj);
        //         if (result != null)
        //         {
        //             return result;
        //         }
        //         return NotFound();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, $"{LocalLog}[Get]");
        //         throw ex;
        //     }

        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventUser>>> GetAll()
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
                _logger.LogError(ex, $"{LocalLog}[GetAll]");
                throw ex;
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]EventUser item)
        {
            try
            {
                await _context.Add(item);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]EventUser item)
        {
            try
            {                
                var keys = new object[]{ item.EventId, item.UserId };                
                var getItem = await _context.GetByIdCompositeKey(keys);
                if (getItem != null)
                {
                    await _context.Update(item);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EventUser>> Delete(int id)
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
                _logger.LogError(ex, $"{LocalLog} [Delete] [Id: {id}]");
                throw ex;
            }

        }
    }
}