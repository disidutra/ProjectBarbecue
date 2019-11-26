using ApplicationCore.Interfaces.Repositorys;
using Barbecue.ApplicationCore.Entities;
using Barbecue.ApplicationCore.Interfaces.Repositorys;
using Barbecue.Infrastructure.Data;

namespace Barbecue.Infrastructure.Repositorys
{
    public class UserRepository : EfBaseRepository<User>, IUserRepository
    {
        public UserRepository(EfBaseContext dbContext) : base(dbContext)
        {
        }
    }
}