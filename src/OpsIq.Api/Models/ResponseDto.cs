namespace OpsIq.Api.Models;

public class CreateTenantResponseDto
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;    // raw sk_ — shown ONCE                                   
    public Guid AdminId { get; set; }
    public string AdminEmail { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
