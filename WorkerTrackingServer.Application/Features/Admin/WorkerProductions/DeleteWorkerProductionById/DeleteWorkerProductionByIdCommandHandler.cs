using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.DeleteWorkerProductionById;
internal sealed class DeleteWorkerProductionByIdCommandHandler(
    IWorkerProductionRepository workerProductionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteWorkerProductionByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteWorkerProductionByIdCommand request, CancellationToken cancellationToken)
    {
        WorkerProduction workerProduction = await workerProductionRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (workerProduction is null)
        {
            return Result<string>.Failure("Worker production not found");
        }

        workerProduction.IsDeleted = true;
        workerProductionRepository.Update(workerProduction);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Worker production deleted successfully");
    }
}
