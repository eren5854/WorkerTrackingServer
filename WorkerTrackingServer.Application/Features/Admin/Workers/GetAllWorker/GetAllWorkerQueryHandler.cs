using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.GetAllWorker;
internal sealed class GetAllWorkerQueryHandler(
    IAppUserRepository appUserRepository) : IRequestHandler<GetAllWorkerQuery, Result<List<AppUser>>>
{
    public async Task<Result<List<AppUser>>> Handle(GetAllWorkerQuery request, CancellationToken cancellationToken)
    {
        List<AppUser> appUsers = await appUserRepository.GetAll().Where(w => w.Role == 3).OrderBy(o => o.CreatedDate).ToListAsync(cancellationToken);
        return Result<List<AppUser>>.Succeed(appUsers);
    }
}
