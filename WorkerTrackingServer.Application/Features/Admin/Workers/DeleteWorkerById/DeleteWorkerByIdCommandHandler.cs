using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.DeleteWorkerById;
internal sealed class DeleteWorkerByIdCommandHandler(
    IAppUserRepository appUserRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteWorkerByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteWorkerByIdCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = await appUserRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (appUser is null)
        {
            return Result<string>.Failure("Worker not found");
        }

        if (appUser.Role == 1 || appUser.Role == 2)
        {
            return Result<string>.Failure("Unauthorized");
        }

        appUser.IsDeleted = true;
        appUserRepository.Update(appUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Worker deleted successfully");
    }
}
