using EventPad.Api.Context.Entities;

namespace EventPad.Api.Service.Users;
public interface IRightsService
{
    Task<bool> IsAdmin(Guid userId);
    Task SetRights(Guid id, UserRole role);
}
