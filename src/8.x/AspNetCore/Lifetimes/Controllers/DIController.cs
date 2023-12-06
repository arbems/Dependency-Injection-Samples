using Lifetimes;
using Microsoft.AspNetCore.Mvc;

namespace DbContext.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DIController(ILogger<DIController> logger,
                      IOperationTransient transientOperation,
                      IOperationScoped scopedOperation,
                      IOperationSingleton singletonOperation) : ControllerBase
    {
        private readonly ILogger _logger = logger;
        private readonly IOperationTransient _transientOperation = transientOperation;
        private readonly IOperationSingleton _singletonOperation = singletonOperation;
        private readonly IOperationScoped _scopedOperation = scopedOperation;

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("Transient: " + _transientOperation.OperationId);
            _logger.LogInformation("Scoped: " + _scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + _singletonOperation.OperationId);

            return Ok();
        }
    }
}
