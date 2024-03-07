namespace EventPad.Api.Services.Actions;

public interface IAction
{
    Task CreateEventAccount(CreateEventAccount model);
    Task CreateUserAccount(CreateUserAccount model);
}
