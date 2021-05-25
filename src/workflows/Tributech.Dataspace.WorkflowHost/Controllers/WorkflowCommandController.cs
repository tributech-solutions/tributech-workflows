using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tributech.Dataspace.WorkflowHost.Controllers {
    [ApiController]
    public class WorkflowCommandController : ControllerBase {

        private readonly ILogger<WorkflowCommandController> _logger;

        public WorkflowCommandController(ILogger<WorkflowCommandController> logger) {
            _logger = logger;
        }

        [HttpPost]
        [Route("/stop")]
        public void Stop() {
            _logger.LogInformation(nameof(Stop));
        }
    }
}
