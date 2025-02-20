using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.EmailSettings;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.GetAllEmailSetting;
internal sealed class GetAllEmailSettingQueryHandler(
    IEmailSettingRepository emailSettingRepository) : IRequestHandler<GetAllEmailSettingQuery, Result<List<EmailSetting>>>
{
    public async Task<Result<List<EmailSetting>>> Handle(GetAllEmailSettingQuery request, CancellationToken cancellationToken)
    {
        List<EmailSetting> emailSettings = await emailSettingRepository.GetAll().OrderBy(o => o.CreatedDate).ToListAsync(cancellationToken);
        return Result<List<EmailSetting>>.Succeed(emailSettings);
    }
}
