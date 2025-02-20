using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.EmailSettings;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.GetAllEmailSetting;
public sealed record GetAllEmailSettingQuery() : IRequest<Result<List<EmailSetting>>>;
