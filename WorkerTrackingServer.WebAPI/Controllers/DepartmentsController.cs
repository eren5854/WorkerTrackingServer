using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.Departments.CreateDepartment;
using WorkerTrackingServer.Application.Features.Admin.Departments.DeleteDepartmentById;
using WorkerTrackingServer.Application.Features.Admin.Departments.GetAllDepartment;
using WorkerTrackingServer.Application.Features.Admin.Departments.UpdateDepartment;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

//[Authorize(AuthenticationSchemes = "Bearer")]
//[Authorize(Roles = "MasterAdmin, Admin")]
public sealed class DepartmentsController : ApiController
{
    public DepartmentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllDepartmentQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteDepartmentByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
