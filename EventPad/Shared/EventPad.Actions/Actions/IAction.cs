namespace EventPad.Services.Actions;

public interface IAction
{
    Task CreateEventAccount(CreateEventAccount model);
    Task CreateUserAccount(CreateUserAccount model);
    Task DeleteEventAccount(Guid id);
    Task DeleteUserAccount(Guid id);
}
