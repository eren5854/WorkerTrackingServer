using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorkerStatus;
internal sealed class UpdateWorkerStatusCommandHandler(
    IAppUserRepository appUserRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateWorkerStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateWorkerStatusCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = await appUserRepository.GetByExpressionAsync(g => g.Id ==  request.Id, cancellationToken);
        if (appUser is null)
        {
            return Result<string>.Failure("Worker not found");
        }

        appUser.IsActive = !appUser.IsActive;

        appUserRepository.Update(appUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Worker status update successfully");
    }
}
