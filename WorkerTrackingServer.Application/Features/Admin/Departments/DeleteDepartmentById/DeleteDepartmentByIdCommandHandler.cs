using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.DeleteDepartmentById;
internal sealed class DeleteDepartmentByIdCommandHandler(
    IDepartmentRepository departmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepartmentByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDepartmentByIdCommand request, CancellationToken cancellationToken)
    {
        Department department = await departmentRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (department is null)
        {
            return Result<string>.Failure("Department not found");
        }

        department.IsDeleted = true;

        departmentRepository.Update(department);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Department deleted successfully");
    }
}
