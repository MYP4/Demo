using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Configuration;
using EventPad.Api.Controllers.Tickets;
using EventPad.Api.Services.Tickets;
using EventPad.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
//[Authorize(policy: AppScopes.Admin)]
[Route("v{version:apiVersion}/[controller]")]
public class TicketController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly ITicketService ticketService;
    private readonly IMapper mapper;

    public TicketController(IAppLogger logger, ITicketService ticketService, IMapper mapper)
    {
        this.logger = logger;
        this.ticketService = ticketService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<TicketResponce>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] TicketFilterRequest filter = null)
    {
        var result = await ticketService.GetAllTickets(page, pageSize, mapper.Map<TicketModelFilter>(filter));

        return mapper.Map<IEnumerable<TicketResponce>>(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await ticketService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<TicketResponce>(result));
    }

    [HttpPost("")]
    public async Task<TicketResponce> Create(CreateTicketRequest request)
    {
        var result = await ticketService.Create(mapper.Map<CreateTicketModel>(request));

        return mapper.Map<TicketResponce>(result);
    }


    [HttpPut("{id:Guid}")]
    public async Task<TicketResponce> Update([FromRoute] Guid id, UpdateTicketModel request)
    {
        //var model = mapper.Map<UpdateEventModel>(request);
        var result = await ticketService.Update(id, mapper.Map<UpdateTicketModel>(request));

        return mapper.Map<TicketResponce>(result);
    }


    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await ticketService.Delete(id);
    }
}
