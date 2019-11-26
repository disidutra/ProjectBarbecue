using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Repositorys;
using Barbecue.Infrastructure.Data;

namespace Barbecue.Infrastructure.Repositorys
{
    public class EventRepository : EfBaseRepository<Event>, IEventRepository
    {
        public EventRepository(EfBaseContext dbContext) : base(dbContext)
        {
        }
        
    }
}