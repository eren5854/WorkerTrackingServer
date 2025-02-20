using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.EmailSettings;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.EmailSettings.DeleteEmailSettingById;
internal sealed class DeleteEmailSettingByIdCommandHandler(
    IEmailSettingRepository emailSettingRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteEmailSettingByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteEmailSettingByIdCommand request, CancellationToken cancellationToken)
    {
        EmailSetting emailSetting = await emailSettingRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (emailSetting is null)
        {
            return Result<string>.Failure("Email setting not found");
        }

        emailSetting.IsDeleted = true;

        emailSettingRepository.Update(emailSetting);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Email setting delete successfully");
    }
}
