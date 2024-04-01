using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Services.Events;
using EventPad.Common.Extensions;
using EventPad.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers.Events;


[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/event")]
public class EventController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IEventService eventService;
    private readonly IMapper mapper;

    public EventController(IAppLogger logger, IEventService eventService, IMapper mapper)
    {
        this.logger = logger;
        this.eventService = eventService;
        this.mapper = mapper;
    }


    [HttpGet("")]
    public async Task<IEnumerable<EventResponse>> GetAllEvents([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] EventFilterRequest filter = null)
    {
        var result = await eventService.GetAllEvents(page, pageSize, mapper.Map<EventModelFilter>(filter));

        return mapper.Map<IEnumerable<EventResponse>>(result);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await eventService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<EventResponse>(result));
    }


    [HttpPost("")]
    [Authorize]
    public async Task<EventResponse> Create(CreateEventRequest request)
    {
        var userId = User.GetUserGuid();

        var model = mapper.Map<CreateEventModel>(request);

        model.AdminId = userId;

        var result = await eventService.Create(model);

        return mapper.Map<EventResponse>(result);
    }


    [HttpPut("{id:Guid}")]
    [Authorize]
    public async Task<EventResponse> Update([FromRoute] Guid id, UpdateEventRequest request)
    {
        var userId = User.GetUserGuid();

        var result = await eventService.Update(id, mapper.Map<UpdateEventModel>(request), userId);

        return mapper.Map<EventResponse>(result);
    }


    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task Delete([FromRoute] Guid id)
    {
        var userId = User.GetUserGuid();

        await eventService.Delete(id, userId);
    }
}




