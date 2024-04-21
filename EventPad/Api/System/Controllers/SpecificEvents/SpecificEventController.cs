using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Controllers.Events;
using EventPad.Api.Services.Events;
using EventPad.Api.Services.Specific;
using EventPad.Common.Extensions;
using EventPad.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers.Specific;


[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/specific-event")]
public class SpecificEventController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly ISpecificEventService specificEventService;
    private readonly IMapper mapper;

    public SpecificEventController(IAppLogger logger, ISpecificEventService specificEventService, IMapper mapper)
    {
        this.logger = logger;
        this.specificEventService = specificEventService;
        this.mapper = mapper;
    }


    [HttpGet("")]
    [Authorize]
    public async Task<IEnumerable<SpecificResponse>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] SpecificEventModelFilter filter = null)
    {
        var userId = User.GetUserGuid();

        var result = await specificEventService.GetAllSpecificEvents(userId, page, pageSize, mapper.Map<SpecificEventModelFilter>(filter));

        return mapper.Map<IEnumerable<SpecificResponse>>(result);
    }


    [HttpGet("event/{id:Guid}")]
    [Authorize]
    public async Task<IEnumerable<SpecificResponse>> GetCurrentSpecificEventsUserEvents(Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userId = User.GetUserGuid();

        var result = await specificEventService.GetCurrentSpecificEvents(userId, id, page, pageSize);

        return mapper.Map<IEnumerable<SpecificResponse>>(result);
    }


    [HttpGet("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await specificEventService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<SpecificResponse>(result));
    }


    [HttpGet("search/{code}")]
    [Authorize]
    public async Task<IActionResult> GetByCode([FromRoute] string code)
    {
        var result = await specificEventService.GetByCode(code);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<SpecificResponse>(result));
    }


    [HttpPost("")]
    [Authorize]
    public async Task<SpecificResponse> Create(CreateSpecificRequest request)
    {
        var userId = User.GetUserGuid();

        var result = await specificEventService.Create(mapper.Map<CreateSpecificModel>(request), userId);

        return mapper.Map<SpecificResponse>(result);
    }


    [HttpPut("{id:Guid}")]
    [Authorize]
    public async Task<SpecificResponse> Update([FromRoute] Guid id, UpdateSpecificRequest request)
    {
        var userId = User.GetUserGuid();

        var result = await specificEventService.Update(id, mapper.Map<UpdateSpecificEventModel>(request), userId);

        return mapper.Map<SpecificResponse>(result);
    }


    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task Delete([FromRoute] Guid id)
    {
        var userId = User.GetUserGuid();

        await specificEventService.Delete(id, userId);
    }
}
