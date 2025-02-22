using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.WorkerProductions.CreateWorkerProduction;
using WorkerTrackingServer.Application.Features.Admin.WorkerProductions.DeleteWorkerProductionById;
using WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetAllWorkerProductionByAppUserId;
using WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetWorkerProductionById;
using WorkerTrackingServer.Application.Features.Admin.WorkerProductions.UpdateWorkerProduction;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

public sealed class WorkerProductionsController : ApiController
{
    public WorkerProductionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkerProductionCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllWorkerProductionByAppUserIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetWorkerProductionByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateWorkerProductionCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteWorkerProductionByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
