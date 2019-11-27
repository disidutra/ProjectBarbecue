using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Barbecue.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEfBaseRepository<Event> _eventRepository;
        private readonly IEfBaseRepository<EventUser> _eventUserRepository;        
        private const string LocalLog = "[WebAPI][EventController]";
        public EventController(
            ILogger<EventController> logger, 
            IEfBaseRepository<Event> EventRepository,            
            IEfBaseRepository<EventUser> eventUserRepository
            )
        {
            _logger = logger;
            _eventRepository = EventRepository;            
            _eventUserRepository = eventUserRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            try
            {
                var result = await _eventRepository.GetById(id);
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
                var result = await _eventRepository.GetAll();
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
                await _eventRepository.Add(item);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]Event item)
        {
            try
            {
                var getItem = await _eventRepository.GetById(item.Id);
                if (getItem != null)
                {
                    await _eventRepository.Update(item);
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

        [HttpPut, Route("AndEventUsers")]
        public async Task<ActionResult> PutEventAndEventUsers([FromBody]Event item)
        {
            try
            {
                var getItem = await _eventRepository.GetAll(x => x.Include(y => y.EventUsers).ThenInclude(y => y.User).Where(x => x.Id == item.Id));
                if (getItem.Any())
                {
                    await _eventRepository.Update(item);
                    await _eventUserRepository.UpdateManyToMany(getItem.First().EventUsers, item.EventUsers);

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
                var item = await _eventRepository.GetById(id);
                if (item != null)
                {
                    await _eventRepository.Remove(item);
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