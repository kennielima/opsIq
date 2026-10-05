

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpsIq.Api.Models;
using OpsIq.Core.Entities;
using OpsIq.Infrastructure.Data;

namespace OpsIq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TenantsController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    public TenantsController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [Route("{tenantId}/users")]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public async Task<IActionResult> CreateUser(Guid tenantId, [FromBody] CreateUserDto createUserDto)
    {
        var claimTenantId = User.FindFirst("TenantId")?.Value;
        if (claimTenantId != tenantId.ToString()) return StatusCode(403, "Tenant ID mismatch.");

        var user = new AppUser
        {
            Email = createUserDto.Email,
            UserName = createUserDto.Email,
            DisplayName = createUserDto.DisplayName,
            TenantId = tenantId
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors.Select(e => e.Description));
        return Ok(user);
    }
}