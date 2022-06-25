using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infrastructure.Domain.Repositories
{
    public partial class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
