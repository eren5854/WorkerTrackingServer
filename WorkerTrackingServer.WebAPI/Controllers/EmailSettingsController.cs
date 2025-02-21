using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerTrackingServer.Application.Features.Admin.EmailSettings.CreateEmailSetting;
using WorkerTrackingServer.Application.Features.Admin.EmailSettings.DeleteEmailSettingById;
using WorkerTrackingServer.Application.Features.Admin.EmailSettings.GetAllEmailSetting;
using WorkerTrackingServer.Application.Features.Admin.EmailSettings.UpdateEmailSetting;
using WorkerTrackingServer.WebAPI.Abstractions;

namespace WorkerTrackingServer.WebAPI.Controllers;

public sealed class EmailSettingsController : ApiController
{
    public EmailSettingsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmailSettingCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllEmailSettingQuery(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateEmailSettingCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteEmailSettingByIdCommand(Id), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
