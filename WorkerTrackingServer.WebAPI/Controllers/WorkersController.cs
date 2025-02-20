using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.Workers.DeleteWorkerById;
using WorkerTrackingServer.Application.Features.Admin.Workers.GetAllWorker;
using WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorker;
using WorkerTrackingServer.Application.Features.Admin.Workers.WorkerRegister;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;


[Authorize(AuthenticationSchemes = "Bearer")]
[Authorize(Roles = "MasterAdmin, Admin")]
public sealed class WorkersController : ApiController
{
    public WorkersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(WorkerRegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllWorkerQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateWorkerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteWorkerByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
