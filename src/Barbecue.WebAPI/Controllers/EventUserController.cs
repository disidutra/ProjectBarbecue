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
        private readonly IEfBaseRepository<EventUser> _eventUserRepository;
        private const string LocalLog = "[WebAPI][EventUserController]";
        public EventUserController(
            ILogger<EventUserController> logger,
            IEfBaseRepository<EventUser> eventUserRepository
        )
        {
            _logger = logger;
            _eventUserRepository = eventUserRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventUser>>> GetAll()
        {
            try
            {
                var result = await _eventUserRepository.GetAll();
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

        [HttpGet("EventId/{id}")]
        public async Task<ActionResult<IEnumerable<EventUser>>> GetAllByEventId(int id)
        {
            try
            {
                var result = await _eventUserRepository.GetAll(f => f.Where(x => x.EventId == id));
                if (result != null)
                {
                    return result.ToList();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Get]");
                throw ex;
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]EventUser item)
        {
            try
            {
                await _eventUserRepository.Add(item);
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
                var keys = new object[] { item.EventId, item.UserId };
                var getItem = await _eventUserRepository.GetByIdCompositeKey(keys);
                if (getItem != null)
                {
                    await _eventUserRepository.Update(item);
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
                var item = await _eventUserRepository.GetById(id);
                if (item != null)
                {
                    await _eventUserRepository.Remove(item);
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