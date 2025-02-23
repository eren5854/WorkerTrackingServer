using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.UpdateWorkerProduction;
internal sealed class UpdateWorkerProductionCommandHandler(
    IWorkerProductionRepository workerProductionRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateWorkerProductionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateWorkerProductionCommand request, CancellationToken cancellationToken)
    {
        bool isAppUserIdAndProductIdExists = await workerProductionRepository.AnyAsync(a => a.Id != request.Id && a.AppUserId == request.AppUserId && a.ProductId == request.ProductId, cancellationToken);

        if (isAppUserIdAndProductIdExists)
        {
            return Result<string>.Failure("This worker production already exists");
        }

        WorkerProduction workerProduction = await workerProductionRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (workerProduction is null)
        {
            return Result<string>.Failure("Worker production not found");
        }

        mapper.Map(request, workerProduction);
        workerProduction.UpdatedDate = DateTime.Now;
        workerProduction.UpdatedBy = "Admin";

        workerProductionRepository.Update(workerProduction);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Worker production update successfully");
    }
}
