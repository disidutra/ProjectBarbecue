using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Services;

namespace Barbecue.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IEfBaseRepository<User> _userRepositoy;
        public UserService(IEfBaseRepository<User> userRepositoy)
        {
            _userRepositoy = userRepositoy;
        }
        public async Task<bool> IsValid(User user){

            var result = await _userRepositoy.GetAll(f => f.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)));

            return result.Any();
        }
    }
}