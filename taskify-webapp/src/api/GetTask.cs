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
  using TaskifyAPI.Managers;
  using TaskifyAPI.Dtos;

  public class GetTask
  {
    private readonly ITaskifyManager Manager;
    public GetTask(ITaskifyManager manager)
    {
      Manager = manager;
    }

    [FunctionName("GetTask")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "task/{id}/{parentId}")] HttpRequest req,
        ILogger log,
        Guid id,
        Guid parentId)
    {
      try
      {
        var claims = AuthUtils.Parse(req);
        req.AddUserIdTelemetry(claims);
        var result = await Manager.GetTaskDetailsAsync(new TaskKey(id, parentId));
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
