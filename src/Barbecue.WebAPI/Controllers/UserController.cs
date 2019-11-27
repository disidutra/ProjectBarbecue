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
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEfBaseRepository<User> _userRepository;
        private const string LocalLog = "[WebAPI][UserController]";
        public UserController(
            ILogger<UserController> logger,
            IEfBaseRepository<User> userRepository
        )
        {
            _logger = logger;
            _userRepository = userRepository;
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
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
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
                await _userRepository.Add(item);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{LocalLog}[Post][Item: {JsonConvert.SerializeObject(item)}]");
                throw ex;
            }
        }
    }
}