using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.UpdateEmailSetting;
public sealed record UpdateEmailSettingCommand(
    Guid Id,
    string Email,
    string AppPassword,
    string SmtpDomainName,
    int SmtpPort) : IRequest<Result<string>>;
