using System.Threading.Tasks;
using Barbecue.ApplicationCore.Entities;

namespace Barbecue.ApplicationCore.Interfaces.Services
{
    public interface IEventService
    {
         Task AddEventAndUsers(Event entite);
    }
}