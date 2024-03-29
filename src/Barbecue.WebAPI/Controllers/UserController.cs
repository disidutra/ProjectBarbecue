using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Services;
using Barbecue.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Barbecue.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEfBaseRepository<User> _userRepository;
        private readonly IUserService _userService;
        private const string LocalLog = "[WebAPI][UserController]";
        public UserController(
            ILogger<UserController> logger,
            IEfBaseRepository<User> userRepository,
            IUserService userService
        )
        {
            _logger = logger;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var result = await _userRepository.GetById(id);
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

        [HttpGet("{id}/IncludeEvents")]
        public async Task<ActionResult<User>> GetIncludeEvents(int id)
        {
            try
            {
                var result = await _userRepository.GetAll(e => 
                    e.Include(u => u.EventUsers)
                    .ThenInclude(e => e.Event)
                    .Where(u => u.Id == id)
                );
                if (result.Any())
                {
                    return result.FirstOrDefault();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[GetIncludeEvents]");
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var result = await _userRepository.GetAll();
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
        public async Task<ActionResult> Post([FromBody]User item)
        {
            try
            {
                await _userRepository.Add(item);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]User item)
        {
            try
            {
                var getItem = await _userRepository.GetById(item.Id);
                if (getItem != null)
                {
                    await _userRepository.Update(item);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Put][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            try
            {
                var item = await _userRepository.GetById(id);
                if (item != null)
                {
                    await _userRepository.Remove(item);
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

        [HttpPost, Route("IsValid")]
        public async Task<ActionResult> IsValid([FromBody]User item)
        {
            try
            {
                var result = await _userService.IsValid(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }
    }
}