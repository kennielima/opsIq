using Microsoft.AspNetCore.Identity;

namespace OpsIq.Core.Entities;

public class AppUser : IdentityUser<Guid>
{
    public Guid TenantId { get; set; }
    public string DisplayName { get; set; } = String.Empty;
}