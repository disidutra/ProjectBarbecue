using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Services;

namespace Barbecue.ApplicationCore.Services
{
    public class EventService : IEventService
    {
        private readonly IEfBaseRepository<Event> _eventRepository;
        private readonly IEfBaseRepository<EventUser> _eventUserRepository;
        public EventService(
            IEfBaseRepository<Event> eventRepository,
            IEfBaseRepository<EventUser> eventUSerRepository
        )
        {
            _eventRepository = eventRepository;
            _eventUserRepository = eventUSerRepository;
        }
        public async Task AddEventAndUsers(Event entity)
        {
            // var eventAdd = entity;
            // eventAdd.EventUSers = null;
            // var eventUsersAdd = new List<EventUser>();
            // var result = await _eventRepository.Add(entity);
            // entity.EventUSers.ToList().ForEach(item =>
            // {
            //     item.EventId = result.Id
            // });
            // await _eventUserRepository.AddRange(entity.EventUSers);
        }

    }
}