using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.EmailSettings;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.UpdateEmailSetting;
internal sealed class UpdateEmailSettingCommandHandler(
    IEmailSettingRepository emailSettingRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateEmailSettingCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateEmailSettingCommand request, CancellationToken cancellationToken)
    {
        EmailSetting emailSetting = await emailSettingRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (emailSetting is null)
        {
            return Result<string>.Failure("Email sertting not found");
        }

        mapper.Map(request, emailSetting);
        emailSetting.UpdatedBy = "Admin";
        emailSetting.UpdatedDate = DateTime.Now;

        emailSettingRepository.Update(emailSetting);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Email setting update successfully");
    }
}
