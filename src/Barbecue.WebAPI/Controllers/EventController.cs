using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Barbecue.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEfBaseRepository<Event> _context;
        private readonly IEventService _service;
        private const string LocalLog = "[WebAPI][EventController]";
        public EventController(
            ILogger<EventController> logger, 
            IEfBaseRepository<Event> context,
            IEventService service)
        {
            _logger = logger;
            _context = context;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
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
                _logger.LogError(ex, $"{LocalLog}[Get]");
                throw ex;
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAll()
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
        public async Task<ActionResult> Post([FromBody]Event item)
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

        [HttpPost, Route("Users")]
        public async Task<ActionResult> PostEventAndUsers(int id, [FromBody]IEnumerable<EventUser> item)
        {
            try
            {
                await _service.AddEventAndUsers(id, item);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[PostEventAndUsers][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromBody]Event item)
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
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> Delete(int id)
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