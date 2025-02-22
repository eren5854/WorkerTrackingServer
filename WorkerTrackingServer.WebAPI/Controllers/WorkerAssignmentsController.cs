using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.CreateWorkerAssignment;
using WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.DeleteWorkerAssignmentById;
using WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetAllWorkerAssignment;
using WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.GetWorkerAssignmentById;
using WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateStatusWorkerAssignment;
using WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateWorkerAssignment;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

public sealed class WorkerAssignmentsController : ApiController
{
    public WorkerAssignmentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkerAssignmentCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllWorkerAssignmentQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetWorkerAssignmentByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateWorkerAssignmentCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateStatus(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateStatusWorkerAssignmentCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteWorkerAssignmentByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
