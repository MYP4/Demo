using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Controllers.Tickets;
using EventPad.Api.Services.Tickets;
using EventPad.Common.Extensions;
using EventPad.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/ticket")]
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
    [Authorize]
    public async Task<IEnumerable<TicketResponce>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] TicketFilterRequest filter = null)
    {
        var result = await ticketService.GetAllTickets(page, pageSize, mapper.Map<TicketModelFilter>(filter));

        return mapper.Map<IEnumerable<TicketResponce>>(result);
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IEnumerable<TicketResponce>> GetUserTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userId = User.GetUserGuid();

        var result = await ticketService.GetUserTickets(userId, page, pageSize);

        return mapper.Map<IEnumerable<TicketResponce>>(result);
    }

    [HttpGet("specific")]
    [Authorize]
    public async Task<IEnumerable<TicketResponce>> GetSpecificTickets(Guid specificEvent, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await ticketService.GetSpecificTickets(specificEvent, page, pageSize);

        return mapper.Map<IEnumerable<TicketResponce>>(result);
    }

    [HttpGet("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var userId = User.GetUserGuid();

        var result = await ticketService.GetById(id, userId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<TicketResponce>(result));
    }

    [HttpPost("")]
    [Authorize]
    public async Task<TicketResponce> Create(CreateTicketRequest request)
    {
        var userId = User.GetUserGuid();

        request.UserId = userId;

        var result = await ticketService.Create(mapper.Map<CreateTicketModel>(request));

        return mapper.Map<TicketResponce>(result);
    }


    [HttpPut("{id:Guid}")]
    [Authorize]
    public async Task<TicketResponce> Update([FromRoute] Guid id, UpdateTicketModel request)
    {
        var userId = User.GetUserGuid();

        var result = await ticketService.Update(id, mapper.Map<UpdateTicketModel>(request), userId);

        return mapper.Map<TicketResponce>(result);
    }


    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task Delete([FromRoute] Guid id)
    {
        var userId = User.GetUserGuid();

        await ticketService.Delete(id, userId);
    }
}
