using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace BlogPOC.UserAPI.Controllers;

[Route("/api/[controller]")]
[ApiController]
[AllowAnonymous]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        this.healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        HealthReport report = await this.healthCheckService.CheckHealthAsync();
        var result = new
        {
            status = report.Status.ToString(),
            errors = report.Entries.Select(e => new { name = e.Key, status = e.Value.Status.ToString(), description = e.Value.Description?.ToString() })
        };
        return report.Status == HealthStatus.Healthy ? this.Ok(result) : this.StatusCode((int)HttpStatusCode.ServiceUnavailable, result);
    }
}