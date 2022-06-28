using Application.Dtos;
using Application.IAppServices;
using Application.Specifications;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApi.Filters;
using WebApi.Parameters;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("bug")]
    [CheckHttpMethodFilter]
    public class BugController : ApiBaseController<BugDto, int>
    {
        private readonly IBugAppService _BugAppService;
        private readonly IMapper _mapper;
        public BugController(IBugAppService appService, ILogger<BugController> logger, IPropertyCheckerService propertyCheckerService,
            IBugAppService BugAppService, IMapper mapper)
            : base(appService, logger, propertyCheckerService)
        {
            _BugAppService = BugAppService;
            _mapper = mapper;
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, BugDto item)
        {
            if (id == 0)
                return BadRequest();
            var itemTarget = await AppService.GetAsync(id);
            if (itemTarget == null)
                return NotFound();
            item.CreationDate = itemTarget.CreationDate;
            item.Id = id;
            var result = await AppService.UpdateAsync(item);
            return NoContent();
        }

        [CheckHttpMethodFilter]
        [HttpHead]
        [HttpGet]
        [Route("~/bugs")]
        public async Task<IActionResult> GetBugs([FromQuery] QueryStringParameters queryStringParameters,
            [FromQuery] BugQueryStringParameters bugQuery)
        {
            if (!bugQuery.IsValid)
                return BadRequest();
            var bugsByUser = new BugsByUserSpecification(_mapper, bugQuery.UserId);
            var bugsByProject = new BugsByProjectSpecification(_mapper, bugQuery.ProjectId);
            var bugsByRange = new BugsByDateRangeSpecification(_mapper, bugQuery.StartDate, bugQuery.EndDate);
            var bugsFiltered = bugsByUser.And(bugsByProject).And(bugsByRange);
            var bugs = await AppService.FindAllBySpecificationPatternAsync(bugsFiltered);
            if (bugs == null || bugs.Count == 0)
                return NotFound();
            return Ok(bugs);
        }

        [HttpPost]
        [HttpPut]
        [HttpPatch]
        [HttpDelete]
        [Route("~/bugs", Order = 2)]
        public IActionResult Post()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        [HttpOptions]
        [Route("~/bugs")]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }

    }
}
