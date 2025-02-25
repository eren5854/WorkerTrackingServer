using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.WorkerAssignments;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateWorkerAssignment;
internal sealed class UpdateWorkerAssignmentCommandHandler(
    IWorkerAssignmentRepository workerAssignmentRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateWorkerAssignmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateWorkerAssignmentCommand request, CancellationToken cancellationToken)
    {
        WorkerAssignment workerAssignment = await workerAssignmentRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);

        if (workerAssignment is null)
        {
            return Result<string>.Failure("Worker assignment not found");
        }

        //bool isWorkerAssignmentExistsToday = await workerAssignmentRepository.AnyAsync(a => a.StartTime.Date == request.StartTime.Date);

        //if (isWorkerAssignmentExistsToday)
        //{
        //    return Result<string>.Failure("Worker assigment already exists today");
        //}

        mapper.Map(request, workerAssignment);
        workerAssignment.UpdatedDate = DateTime.Now;
        workerAssignment.UpdatedBy = "Admin";

        workerAssignmentRepository.Update(workerAssignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Worker assignment update successfully");
    }
}
