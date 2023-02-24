using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.Interfaces;
using AuthenticationAPI.Infrastructure.Contexts;

namespace AuthenticationAPI.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
