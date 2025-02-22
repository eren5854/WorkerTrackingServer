using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerLogins.GetAllWorkerLoginByAppUserId;
internal sealed class GetAllWorkerLoginByAppUserIdCommandHandler(
    IWorkerLoginRepository workerLoginRepository) : IRequestHandler<GetAllWorkerLoginByAppUserIdCommand, Result<List<WorkerLogin>>>
{
    public async Task<Result<List<WorkerLogin>>> Handle(GetAllWorkerLoginByAppUserIdCommand request, CancellationToken cancellationToken)
    {
        List<WorkerLogin> workerLogins = await workerLoginRepository.GetAll().Where(w => w.AppUserId == request.AppUserId).Include(i => i.AppUser).OrderByDescending(o => o.CreatedDate).ToListAsync(cancellationToken);

        return Result<List<WorkerLogin>>.Succeed(workerLogins);
    }
}
