using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
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
        bool isWorkerAssignmentExistsToday = await workerAssignmentRepository.AnyAsync(a => a.StartTime.Date == request.StartTime.Date);

        if (isWorkerAssignmentExistsToday)
        {
            return Result<string>.Failure("Worker assigment already exists today");
        }

        WorkerAssignment workerAssignment = mapper.Map<WorkerAssignment>(request);
        workerAssignment.CreatedBy = "Admin";
        workerAssignment.CreatedDate = DateTime.Now;

        await workerAssignmentRepository.AddAsync(workerAssignment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Worker assignment create successfully");
    }
}
