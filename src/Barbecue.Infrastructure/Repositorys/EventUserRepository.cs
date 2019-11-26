using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Repositorys;
using Barbecue.Infrastructure.Data;

namespace Barbecue.Infrastructure.Repositorys
{
    public class EventUserRepository : EfBaseRepository<EventUser>, IEventUserRepository
    {
        public EventUserRepository(EfBaseContext dbContext) : base(dbContext)
        {
        }
        
    }
}