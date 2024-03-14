using AutoMapper;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Pay.Services.EventAccounts;

public class EventAccountModel
{
    public Guid Id { get; set; }
    public string? AccountNumber { get; set; }
    public float Balance { get; set; }
}


public class EventAccountModelProfile : Profile
{
    public EventAccountModelProfile()
    {
        CreateMap<EventAccount, EventAccountModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            ;
    }
}
