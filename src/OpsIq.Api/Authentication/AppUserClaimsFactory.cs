using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OpsIq.Core.Entities;

namespace OpsIq.Api.Authentication;

public class AppUserClaimsFactory
    : UserClaimsPrincipalFactory<AppUser>
{
    public AppUserClaimsFactory(
        UserManager<AppUser> userManager,
        IOptions<IdentityOptions> options)
        : base(userManager, options) { }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
        if (user.TenantId.HasValue)
            identity.AddClaim(new Claim("TenantId", user.TenantId.Value.ToString()));
        return identity;
    }
}