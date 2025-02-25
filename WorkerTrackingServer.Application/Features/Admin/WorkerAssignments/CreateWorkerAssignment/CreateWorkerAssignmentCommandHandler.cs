using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateWorkerAssignment;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.CreateWorkerAssignment;
internal sealed class CreateWorkerAssignmentCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateWorkerAssignmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateWorkerAssignmentCommand request, CancellationToken cancellationToken)
    {
        bool isWorkerAssignmentExists = await workerAssignmentRepository.AnyAsync(a => a.AppUserId == request.AppUserId && a.MachineId == request.MachineId && a.WorkerProductionId == request.WorkerProductionId);

        if (isWorkerAssignmentExists)
        {
            return Result<string>.Failure("Worker assigment already exists");
        }

        WorkerAssignment isWorkerAssignmentExistsWorker = await workerAssignmentRepository.GetByExpressionAsync(a => a.AppUserId == request.AppUserId && a.IsActive, cancellationToken);
        if (isWorkerAssignmentExistsWorker is not null)
        {
            isWorkerAssignmentExistsWorker.IsActive = false;
            workerAssignmentRepository.Update(isWorkerAssignmentExistsWorker);
            //return Result<string>.Failure("Bu için zaten aktif bir atama mevcut. Lütfen çalışanın önceki atamasını kaldırın veya deaktif edin!");
        }

        WorkerAssignment workerAssignment = mapper.Map<WorkerAssignment>(request);
        workerAssignment.CreatedBy = "Admin";
        workerAssignment.CreatedDate = DateTime.Now;

        await workerAssignmentRepository.AddAsync(workerAssignment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Worker assignment create successfully");
    }
}
