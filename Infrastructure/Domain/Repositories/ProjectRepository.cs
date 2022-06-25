using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infrastructure.Domain.Repositories
{
    public partial class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
