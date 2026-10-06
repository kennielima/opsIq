namespace OpsIq.Api.Models;

using System.ComponentModel.DataAnnotations;
using OpsIq.Core.Entities;

public class CreateTenantDto
{
    [Required]
    public string TenantName { get; set; } = string.Empty;
    public string TenantSlug { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string AdminEmail { get; set; } = string.Empty;
    [Required]
    [MinLength(8)]
    public string AdminPassword { get; set; } = string.Empty;
    public string AdminDisplayName { get; set; } = string.Empty;
}