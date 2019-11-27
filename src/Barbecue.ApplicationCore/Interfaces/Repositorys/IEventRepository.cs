using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;

namespace Barbecue.ApplicationCore.Interfaces.Repositorys
{
    public interface IEventRepository : IEfBaseRepository<Event>
    {
         Task UpdateEventAndEventUsers(Event entity);
    }
}