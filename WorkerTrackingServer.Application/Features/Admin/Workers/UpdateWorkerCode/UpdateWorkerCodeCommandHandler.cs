using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Application.Services;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorkerCode;
internal sealed class UpdateWorkerCodeCommandHandler(
    IAppUserRepository appUserRepository,
    IGenerateCode generateCode,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateWorkerCodeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateWorkerCodeCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = await appUserRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (appUser is null)
        {
            return Result<string>.Failure("Worker not found");
        }

        string workerCode;
        do
        {
            workerCode = generateCode.GenerateWorkerCode(cancellationToken);
        } while (await appUserRepository.AnyAsync(x => x.WorkerCode == workerCode, cancellationToken));

        appUser.WorkerCode = workerCode;

        appUserRepository.Update(appUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Worker code updated successfully");
    }
}
