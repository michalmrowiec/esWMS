using esWMS.API.Controllers.Utils;
using esWMS.API.Services;
using esWMS.Application.Functions.Contractors;
using esWMS.Application.Functions.Employees;
using esWMS.Application.Functions.Employees.Command.CreateEmployee;
using esWMS.Application.Functions.Employees.Command.LoginEmployee;
using esWMS.Application.Functions.Employees.Command.UpdateEmployee;
using esWMS.Application.Functions.Employees.Queries.GetEmployeeById;
using esWMS.Application.Functions.Employees.Queries.GetSortedFilteredEmployees;
using esWMS.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace esWMS.API.Controllers.SystemActors
{
    [Authorize]
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

        [Authorize]
        [HttpHead]
        public ActionResult LoginCheck()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeDetails(
            [FromQuery] string employeeId)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(employeeId));

            return result.HandleOkResult(this);
        }

        [HttpPost("get-filtered")]
        public async Task<ActionResult<PagedResult<EmployeeDto>>> GetSortedAndFilteredEmployees(
            [FromBody] SieveModel sieveModel)
        {
            var result = await _mediator.Send(new GetSortedFilteredEmployeessQuery(sieveModel));

            return result.HandleOkResult(this);
        }

        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee
            ([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            if (_userContextService.GetUserId is not null)
                createEmployeeCommand.CreatedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(createEmployeeCommand);

            return result.HandleCreatedResult(this, "");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LogedEmployeeDto>> Login([FromBody] LoginEmployeeCommand loginEmployeeCommand)
        {
            var result = await _mediator.Send(loginEmployeeCommand);

            return result.HandleOkResult(this);
        }

        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPut]
        public async Task<ActionResult<ContractorDto>> UpdateEmployee(
            [FromBody] UpdateEmployeeCommand updateEmployeeCommand)
        {
            if (_userContextService.GetUserId is not null)
                updateEmployeeCommand.ModifiedBy = _userContextService.GetUserId.ToString();

            var result = await _mediator.Send(updateEmployeeCommand);

            return result.HandleOkResult(this);
        }
    }
}
