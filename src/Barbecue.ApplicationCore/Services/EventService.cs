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
        // public async Task AddEventAndUsers(int eventId, IEnumerable<EventUser> entity)
        // {
        //     entity.ToList().ForEach(item => item.EventId = eventId);
        //     await _eventUserRepository.AddRange(entity);
        // }

        // public async Task AddOrUpdateEventAndUsers(int eventId, IEnumerable<EventUser> entity)
        // {
        //     var eventUsersAdd = entity.ToList();
        //     var eventUsersRemove = new List<EventUser>();
        //     var eventUsersUpdate = new List<EventUser>();

        //     var eventUsers = await _eventUserRepository.GetAllWhere(x => x.EventId == eventId);                

        //     eventUsers.ToList().ForEach(item => {
        //         var resultItem = eventUsersAdd.Where(x=> x.UserId == item.UserId).FirstOrDefault();
        //         if (resultItem != null){
        //             eventUsersUpdate.Add(item);
        //             eventUsersAdd.Remove(resultItem);
        //         } else {
        //             eventUsersRemove.Add(item);
        //         }
        //     });

        //     // entity.ToList().ForEach(item => item.EventId = eventId);
        //     // await _eventUserRepository.AddRange(entity);

        // }
    }
}