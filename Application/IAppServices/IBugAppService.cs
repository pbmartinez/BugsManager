using Application.Dtos;

namespace Application.IAppServices
{
    public partial interface IBugAppService : IAppService<BugDto,int>
    {
    }
}
