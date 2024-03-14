using AutoMapper;
using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Services.Transactions;

public class TransactionModel
{
    public Guid Id { get; set; }

    public Guid EventAccountId { get; set; }
    public Guid UserAccountId { get; set; }
    public Guid TicketId { get; set; }

    public TransactionType Type { get; set; }
    public float Amount { get; set; }
    public DateOnly Date {  get; set; }
    public TimeOnly Time { get; set; }
    public string Details { get; set; }
}

public class TransactionModelProfile : Profile
{
    public TransactionModelProfile()
    {
        CreateMap<Transaction, TransactionModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.EventAccountId, opt => opt.MapFrom(src => src.EventAccount.Uid))
            .ForMember(dest => dest.UserAccountId, opt => opt.MapFrom(src => src.UserAccount.Uid))
            .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.Ticket))
            ;
    }
}
