using Application.Dtos;
using Application.IAppServices;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("bugs")]
    public class BugController : ApiBaseController<BugDto, int>
    {
        private readonly IBugAppService _BugAppService;        
        public BugController(IBugAppService appService, ILogger<BugController> logger, IPropertyCheckerService propertyCheckerService,
            IBugAppService BugAppService) 
            : base(appService, logger, propertyCheckerService)
        {
            _BugAppService = BugAppService;
            Includes = new List<string>() { nameof(BugDto.Project), nameof(BugDto.User) };
        }


    }
}
