using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.EmailSettings;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.CreateEmailSetting;
internal sealed class CreateEmailSettingCommandHandler(
    IEmailSettingRepository emailSettingRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateEmailSettingCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateEmailSettingCommand request, CancellationToken cancellationToken)
    {
        List<EmailSetting> emailSettings = await emailSettingRepository.GetAll().Where(w => w.IsActive).ToListAsync(cancellationToken);
        if (emailSettings.Count > 0)
        {
            return Result<string>.Failure("There is already an email setting");
        }

        EmailSetting emailSetting = mapper.Map<EmailSetting>(request);
        emailSetting.CreatedBy = "Admin";
        emailSetting.CreatedDate = DateTime.Now;

        await emailSettingRepository.AddAsync(emailSetting,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Email setting created successfully");
    }
}
