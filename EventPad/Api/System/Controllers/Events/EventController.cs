using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Services.Events;
using EventPad.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers.Events;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
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
    public async Task<EventResponse> Create(CreateEventRequest request)
    {
        var result = await eventService.Create(mapper.Map<CreateEventModel>(request));

        return mapper.Map<EventResponse>(result);
    }


    [HttpPut("{id:Guid}")]
    public async Task<EventResponse> Update([FromRoute] Guid id, UpdateEventRequest request)
    {
        //var model = mapper.Map<UpdateEventModel>(request);
        var result = await eventService.Update(id, mapper.Map<UpdateEventModel>(request));

        return mapper.Map<EventResponse>(result);
    }


    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await eventService.Delete(id);
    }
}




