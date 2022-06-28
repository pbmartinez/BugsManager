using Application.Dtos;
using Application.IAppServices;
using Application.Specifications;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ApiBaseController<UserDto, int>
    {
        private readonly IMapper _mapper;
        public UserController(IUserAppService appService, ILogger<UserController> logger, IPropertyCheckerService propertyCheckerService,
            IMapper mapper)
            : base(appService, logger, propertyCheckerService)
        {
            _mapper = mapper;
        }

        [HttpGet("available")]
        public async Task<ActionResult<UserDto>> GetAvailableUsersAsync()
        {
            var filter = new UsersAvailableForBugSpecification(_mapper);
            var users = await AppService.FindAllBySpecificationPatternAsync(filter);
            return Ok(users);
        }
    }
}
