using System.Threading.Tasks;
using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Repositorys;
using Barbecue.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbecue.Infrastructure.Repositorys
{
    public class EventRepository : EfBaseRepository<Event>, IEventRepository
    {
        public EventRepository(EfBaseContext dbContext, EfBaseRepository<EventUser> Context
        ) : base(dbContext) { }
        public async Task UpdateEventAndEventUsers(Event entity)
        {
            
            // _base_context.Entry(entity).State = EntityState.Modified;



            // await _base_context.SaveChangesAsync();
        }
    }
}