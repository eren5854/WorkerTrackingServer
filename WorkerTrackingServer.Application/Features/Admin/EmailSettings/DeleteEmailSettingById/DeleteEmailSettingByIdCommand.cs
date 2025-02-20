using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.DeleteEmailSettingById;
public sealed record DeleteEmailSettingByIdCommand(Guid Id) : IRequest<Result<string>>;
