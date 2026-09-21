using opsiq.Models;
using opsiq.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace opsiq.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueryController : ControllerBase
{
    private readonly QueryService _queryService;
    public QueryController(QueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet("protected")]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public IActionResult Protected()
    {
        return Ok("You have a valid API key.");
    }
    [HttpPost]
    // [Route("ask")]
    public async Task<ActionResult> GetTaskAsync([FromBody] RequestDto request)
    {
        var prompt = request.Question ?? string.Empty;
        var answer = await _queryService.GetQuestionAsync(prompt);
        return Ok(answer);
    }
}