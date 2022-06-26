using Application.Dtos;
using Application.IAppServices;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("project")]
    [ApiController]
    public class ProjectController : ApiBaseController<ProjectDto, int>
    {
        public ProjectController(IProjectAppService appService, ILogger<ProjectController> logger, IPropertyCheckerService propertyCheckerService) : base(appService, logger, propertyCheckerService)
        {
        }
    }
}
