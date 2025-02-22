using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerLogins.GetAllWorkerLogin;
internal sealed class GetAllWorkerLoginQueryHandler(
    IWorkerLoginRepository workerLoginRepository) : IRequestHandler<GetAllWorkerLoginQuery, Result<List<WorkerLogin>>>
{
    public async Task<Result<List<WorkerLogin>>> Handle(GetAllWorkerLoginQuery request, CancellationToken cancellationToken)
    {
        List<WorkerLogin> workerLogins = await workerLoginRepository.GetAll().Include(i => i.AppUser).OrderByDescending(o => o.CreatedDate).ToListAsync(cancellationToken);

        return Result<List<WorkerLogin>>.Succeed(workerLogins);
    }
}
