using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Controllers.Users;
using EventPad.Api.Service.Users;
using EventPad.Common.Extensions;
using EventPad.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/user")]
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
    [Authorize]
    public async Task<IEnumerable<UserResponse>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] UserFilterRequest filter = null)
    {
        var result = await UserService.GetAllUsers(page, pageSize, mapper.Map<UserModelFilter>(filter));

        return mapper.Map<IEnumerable<UserResponse>>(result);
    }

    [HttpGet("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await UserService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<UserResponse>(result));
    }

    [HttpPost("")]
    public async Task<UserResponse> Create([FromBody] RegisterUserRequest request)
    {
        var result = await UserService.Create(mapper.Map<RegiserUserModel>(request));

        return mapper.Map<UserResponse>(result);
    }


    [HttpPut("{id:Guid}")]
    [Authorize]
    public async Task<UserResponse> Update([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var userId = User.GetUserGuid();

        var result = await UserService.Update(id, mapper.Map<UpdateUserModel>(request), userId);

        return mapper.Map<UserResponse>(result);
    }


    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task Delete([FromRoute] Guid id)
    {
        var userId = User.GetUserGuid();

        await UserService.Delete(id, userId);
    }
}
