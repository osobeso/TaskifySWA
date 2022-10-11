namespace Taskify.Api
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Extensions.Http;
  using Microsoft.Extensions.Logging;
  using System;
  using System.Threading.Tasks;
  using System.Web.Http;
  using TaskifyAPI.Dtos;
  using TaskifyAPI.Managers;

  public class GetRootTask
  {
    private readonly ITaskifyManager Manager;
    public GetRootTask(ITaskifyManager manager)
    {
      Manager = manager;
    }

    [FunctionName("GetRootTask")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "task/{id}")] HttpRequest req,
    ILogger log,
    Guid id)
    {
      try
      {
        var claims = AuthUtils.Parse(req);
        req.AddUserIdTelemetry(claims);
        var result = await Manager.GetTaskDetailsAsync(new TaskKey(id, null));
        return new OkObjectResult(result);
      }
      catch (Exception ex)
      {
        log.LogError("Exception: {Message}", ex.Message);
        return new ExceptionResult(ex, true);
      }
    }
  }
}
