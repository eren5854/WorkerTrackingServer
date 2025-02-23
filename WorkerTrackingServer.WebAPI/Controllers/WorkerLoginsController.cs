using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.WorkerLogins.GetAllWorkerLogin;
using WorkerTrackingServer.Application.Features.Admin.WorkerLogins.GetAllWorkerLoginByAppUserId;
using WorkerTrackingServer.Application.Features.Worker.WorkerActualQuantity;
using WorkerTrackingServer.Application.Features.Worker.WorkerLoginWithWorkerCode;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

public sealed class WorkerLoginsController : ApiController
{
    public WorkerLoginsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllWorkerLoginQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserId(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllWorkerLoginByAppUserIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> WorkerLoginWithCode(string WorkerCode, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new WorkerLoginWithWorkerCodeCommand(WorkerCode), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> WorkerActualQuantity(Guid AppUserId, int ActualQuantity, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new WorkerActualQuantityCommand(AppUserId, ActualQuantity), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
