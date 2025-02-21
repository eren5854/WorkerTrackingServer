using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.CreateDepartmentProduction;
using WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.DeleteDepartmentProductionById;
using WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetAllDepartmentProduction;
using WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetDepartmentProductionById;
using WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.UpdateDepartmentProduction;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

public sealed class DepartmentProductionsController : ApiController
{
    public DepartmentProductionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentProductionCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllDepartmentProductionQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetDepartmentProductionByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateDepartmentProductionCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteDepartmentProductionByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
