using System.Threading.Tasks;
using Barbecue.ApplicationCore.Entities;

namespace Barbecue.ApplicationCore.Interfaces.Services
{
    public interface IUserService
    {
         Task<bool> IsValid(User user);
    }
}