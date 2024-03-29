namespace EventPad.Api.Service.Users;
public interface IProfileService
{
    Task<UserProfileModel> GetProfile(Guid id);
}
