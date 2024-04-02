using Asp.Versioning;
using AutoMapper;
using EventPad.Api.Service.Users;
using EventPad.Common.Extensions;
using EventPad.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPad.Api.Controllers.UserProfile;


[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
//[Authorize(policy: AppScopes.Admin)]
[Route("v{version:apiVersion}/profile")]
public class ProfileController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IProfileService profileService;
    private readonly IMapper mapper;

    public ProfileController(IAppLogger logger, IProfileService profileService, IMapper mapper)
    {
        this.logger = logger;
        this.profileService = profileService;
        this.mapper = mapper;
    }


    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userId = User.GetUserGuid();

        var result = await profileService.GetProfile(userId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<UserProfileResponse>(result));
    }
}
