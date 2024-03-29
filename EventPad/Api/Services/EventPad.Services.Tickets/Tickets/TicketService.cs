﻿
using AutoMapper;
using EventPad.Common;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using Microsoft.EntityFrameworkCore;
using EventPad.Services.Actions;
using EventPad.Actions;
using Microsoft.Extensions.Logging;

namespace EventPad.Api.Services.Tickets;

public class TicketService : ITicketService
{
    private readonly IDbContextFactory<ApiDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateTicketModel> createModelValidator;
    private readonly IModelValidator<UpdateTicketModel> updateModelValidator;
    private readonly IAction action;

    public TicketService(IDbContextFactory<ApiDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateTicketModel> createModelValidator,
        IModelValidator<UpdateTicketModel> updateModelValidator,
        IAction action)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
        this.updateModelValidator = updateModelValidator;
        this.action = action;
    }

    public async Task<IEnumerable<TicketModel>> GetAllTickets(int page = 1, int pageSize = 10, TicketModelFilter filter = null)
    {
        var _event = filter?.EventId;
        var user = filter?.UserId;
        var status = filter?.Status;

        using var context = await dbContextFactory.CreateDbContextAsync();

        var tickets = context.Tickets.AsQueryable();

        if (_event != null)
        {
            tickets = tickets.Where(x => x.SpecificEvent.Event.Uid == _event);
        }
        if (user != null)
        {
            tickets = tickets.Where(x => x.User.Id == user);
        }
        if (status != null)
        {
            tickets = tickets.Where(x => x.Status == status);
        }

        tickets = tickets.Skip((page - 1) * pageSize).Take(pageSize);

        var eventList = await tickets.ToListAsync();

        var result = mapper.Map<IEnumerable<TicketModel>>(eventList);

        return result;
    }

    public async Task<TicketModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var ticket = await context.Tickets.FirstOrDefaultAsync(x => x.Uid == id);

        var result = mapper.Map<TicketModel>(ticket);

        return result;
    }

    public async Task<TicketModel> Create(CreateTicketModel model)
    {
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var specific = context.SpecificEvents.FirstOrDefault(x => x.Uid == model.SpecificId);
        var user = context.Users.FirstOrDefault(x => x.Id == model.UserId);

        var tickets = context.Tickets.Where(x => x.SpecificEvent.Uid == model.SpecificId);

        if (tickets.Count() >= specific.TicketCount)
        {
            throw new ProcessException("Tickets are out");
        }

        var ticket = mapper.Map<Ticket>(model);

        ticket.Uid = Guid.NewGuid();

        await context.Tickets.AddAsync(ticket);

        await action.BuyTicket(new BuyTicket()
        {
            EventAccountId = ticket.SpecificEvent.Uid,
            UserAccountId = ticket.User.Id,
            Ticket = ticket.Uid,
            Amount = ticket.SpecificEvent.Price
        });


        await context.SaveChangesAsync();

        return mapper.Map<TicketModel>(ticket);
    }

    public async Task<TicketModel> Update(Guid id, UpdateTicketModel model)
    {
        await updateModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var ticket = await context.Tickets.FirstOrDefaultAsync(x => x.Uid == id);

        ticket = mapper.Map(model, ticket);

        context.Tickets.Update(ticket);

        await context.SaveChangesAsync();

        return mapper.Map<TicketModel>(ticket);
    }

    // работа с UID
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var ticket = await context.Tickets.FirstOrDefaultAsync(x => x.Uid == id);

        if (ticket == null)
            throw new ProcessException($"Ticket (ID = {id}) not found.");

        await action.RefundTicket(new RefundTicket()
        {
            UserAccountId = ticket.User.Id,
            EventAccountId = ticket.SpecificEvent.Uid,
            Ticket = ticket.Uid,
            Amount = ticket.SpecificEvent.Price
        });

        context.Tickets.Remove(ticket);

        await context.SaveChangesAsync();
    }
}
