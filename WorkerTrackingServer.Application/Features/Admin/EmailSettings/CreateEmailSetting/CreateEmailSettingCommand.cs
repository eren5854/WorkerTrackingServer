using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.CreateEmailSetting;
public sealed record CreateEmailSettingCommand(
    string Email,
    string AppPassword,
    string SmtpDomainName,
    int Port): IRequest<Result<string>>;
