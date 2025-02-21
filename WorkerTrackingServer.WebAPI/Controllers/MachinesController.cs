using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.Machines.CreateMachine;
using WorkerTrackingServer.Application.Features.Admin.Machines.DeleteMachineById;
using WorkerTrackingServer.Application.Features.Admin.Machines.GetAllMachine;
using WorkerTrackingServer.Application.Features.Admin.Machines.GetMachineById;
using WorkerTrackingServer.Application.Features.Admin.Machines.UpdateMachine;
using WorkerTrackingServer.Application.Features.Admin.Machines.UpdateMachineStatus;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

public sealed class MachinesController : ApiController
{
    public MachinesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMachineCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllMachineQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetMachineByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateMachineCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateStatus(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateMachineStatusCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteMachineByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
