using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infrastructure.Domain.Repositories
{
    public partial class BugRepository : Repository<Bug>, IBugRepository
    {
        public BugRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
