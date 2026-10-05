using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsIq.Infrastructure.Data;
using OpsIq.Core.Entities;
namespace OpsIq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "ApiKey")]
public class DocumentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DocumentsController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var tenantId = User.FindFirst("TenantId")?.Value;
        if (string.IsNullOrEmpty(tenantId)) return BadRequest("No tenant");
        if (file == null || file.Length == 0) return BadRequest("No file");
        var documentId = Guid.NewGuid();
        var path = Path.Combine("uploads", $"{documentId}{Path.GetExtension(file.FileName)}");
        Directory.CreateDirectory("uploads");
        using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream);
        var doc = new Document
        {
            Id = documentId,
            TenantId = Guid.Parse(tenantId),
            FileName = file.FileName,
            StoragePath = path,
            Status = DocumentStatus.Processing,
            UploadedAt = DateTime.UtcNow,
            ContentType = file.ContentType
        };
        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();
        return Ok(doc);
        // Accepted(doc);
    }
}