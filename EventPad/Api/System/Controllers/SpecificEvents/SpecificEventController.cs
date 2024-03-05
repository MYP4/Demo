using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Controllers.Events;
using EventPad.Api.Services.Specific;
using EventPad.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers.Specific;


[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
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
    public async Task<IEnumerable<SpecificResponse>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] SpecificEventModelFilter filter = null)
    {
        var result = await specificEventService.GetAllSpecificEvents(page, pageSize, mapper.Map<SpecificEventModelFilter>(filter));

        return mapper.Map<IEnumerable<SpecificResponse>>(result);
    }

    //[HttpGet("")]
    //public async Task<IEnumerable<EventResponse>> GetUsersEvents([FromQuery] Guid id, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    //{
    //    var result = await specificEventService.GetCurrentSpecificEvents(id, page, pageSize);

    //    return mapper.Map<IEnumerable<EventResponse>>(result);
    //}


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await specificEventService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<SpecificResponse>(result));
    }


    [HttpPost("")]
    public async Task<SpecificResponse> Create(CreateSpecificRequest request)
    {
        var result = await specificEventService.Create(mapper.Map<CreateSpecificEventModel>(request));

        return mapper.Map<SpecificResponse>(result);
    }


    [HttpPut("{id:Guid}")]
    public async Task<SpecificResponse> Update([FromRoute] Guid id, UpdateSpecificRequest request)
    {
        //var model = mapper.Map<UpdateEventModel>(request);
        var result = await specificEventService.Update(id, mapper.Map<UpdateSpecificEventModel>(request));

        return mapper.Map<SpecificResponse>(result);
    }


    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await specificEventService.Delete(id);
    }
}
