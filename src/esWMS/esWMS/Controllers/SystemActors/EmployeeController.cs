using esWMS.Application.Functions.Employees;
using esWMS.Application.Functions.Employees.Command.CreateEmployee;
using esWMS.Controllers.Utils;
using esWMS.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace esWMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IUserContextService _userContextService;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee
            ([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            if (_userContextService.GetUserId is not null)
                createEmployeeCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createEmployeeCommand);

            return result.HandleCreatedResult(this, "");
        }
    }
}
