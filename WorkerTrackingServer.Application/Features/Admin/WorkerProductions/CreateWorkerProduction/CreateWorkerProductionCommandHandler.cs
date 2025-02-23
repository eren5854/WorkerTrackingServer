using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.CreateWorkerProduction;
internal sealed class CreateWorkerProductionCommandHandler(
    IWorkerProductionRepository workerProductionRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateWorkerProductionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateWorkerProductionCommand request, CancellationToken cancellationToken)
    {
        bool isAppUserIdAndProductIdExists = await workerProductionRepository.AnyAsync(a => a.AppUserId == request.AppUserId && a.ProductId == request.ProductId, cancellationToken);

        if (isAppUserIdAndProductIdExists)
        {
            return Result<string>.Failure("This worker production already exists");
        }

        WorkerProduction workerProduction = mapper.Map<WorkerProduction>(request);
        workerProduction.CreatedBy = "Admin";
        workerProduction.CreatedDate = DateTime.Now;

        await workerProductionRepository.AddAsync(workerProduction,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Worker production created successfully");
    }
}
