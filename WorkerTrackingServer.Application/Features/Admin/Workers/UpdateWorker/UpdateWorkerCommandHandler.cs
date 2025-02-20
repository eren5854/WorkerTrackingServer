using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorker;
internal sealed class UpdateWorkerCommandHandler(
    IAppUserRepository appUserRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateWorkerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = await appUserRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (appUser is null)
        {
            return Result<string>.Failure("Worker not found");
        }

        if(appUser.Role == 1 || appUser.Role == 2)
        {
            return Result<string>.Failure("Unauthorized");
        }

        if(appUser.UserName!.ToUpper() != request.UserName.ToUpper())
        {
            bool isUserNameAlreadyExists = await appUserRepository.AnyAsync(a => a.UserName == request.UserName, cancellationToken);
            if (isUserNameAlreadyExists)
            {
                return Result<string>.Failure("User Name already exists");
            }

        }

        mapper.Map(request, appUser);
        appUser.UpdatedBy = "Admin";
        appUser.UpdatedDate = DateTime.Now;

        appUserRepository.Update(appUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Worker update successfully");
    }
}
