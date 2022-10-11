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

  public class SetParentTask
  {
    private readonly ITaskifyManager Manager;
    public SetParentTask(ITaskifyManager manager)
    {
      Manager = manager;
    }

    [FunctionName("SetParentTask")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "task/parent")] HttpRequest req,
        ILogger log)
    {
      try
      {
        var dto = await RequestUtils.ParseBodyAsync<SetParentTaskDto>(req);
        var claims = AuthUtils.Parse(req);
        req.AddUserIdTelemetry(claims);
        var result = await Manager.SetParentAsync(dto);
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
