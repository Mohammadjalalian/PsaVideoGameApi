using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PsaVideoGameDataProvider;

namespace PsaVideoGameApi.Controllers
{
  //[Authorize]
  public class PsaControllerBase<T> : ControllerBase where T : ControllerBase 
  {
    protected static readonly string[] ScopeRequiredByApi = { "API.Access" };

    protected ILogger<T> Logger { get;}
    protected IUnitOfWork UnitOfWork { get; }

    public PsaControllerBase(ILogger<T> logger, IUnitOfWork unitOfWork)
    {
      Logger = logger;
      UnitOfWork = unitOfWork;
    }

    protected void VerifyRequest()
    {
     
    }

    public override OkObjectResult Ok(object value)
    {
      VerifyRequest();
      return base.Ok(value);
    }
  }
}
