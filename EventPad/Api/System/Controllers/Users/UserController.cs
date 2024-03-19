using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Controllers.Users;
using EventPad.Api.Services.Users;
using EventPad.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
//[Authorize(policy: AppScopes.Admin)]
[Route("v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IUserService UserService;
    private readonly IMapper mapper;

    public UserController(IAppLogger logger, IUserService UserService, IMapper mapper)
    {
        this.logger = logger;
        this.UserService = UserService;
        this.mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<UserResponce>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] UserFilterRequest filter = null)
    {
        var result = await UserService.GetAllUsers(page, pageSize, mapper.Map<UserModelFilter>(filter));

        return mapper.Map<IEnumerable<UserResponce>>(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await UserService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<UserResponce>(result));
    }

    [HttpPost("")]
    public async Task<UserResponce> Create(RegisterUserRequest request)
    {
        var result = await UserService.Create(mapper.Map<RegiserUserModel>(request));

        return mapper.Map<UserResponce>(result);
    }


    [HttpPut("{id:Guid}")]
    public async Task<UserResponce> Update([FromRoute] Guid id, UpdateUserRequest request)
    {
        //var model = mapper.Map<UpdateEventModel>(request);
        var result = await UserService.Update(id, mapper.Map<UpdateUserModel>(request));

        return mapper.Map<UserResponce>(result);
    }


    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await UserService.Delete(id);
    }
}
