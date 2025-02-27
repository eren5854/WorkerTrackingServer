using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.GetDepartmentById;
internal sealed class GetDepartmentByIdCommandHandler(
    IDepartmentRepository departmentRepository) : IRequestHandler<GetDepartmentByIdCommand, Result<Department>>
{
    public async Task<Result<Department>> Handle(GetDepartmentByIdCommand request, CancellationToken cancellationToken)
    {
        Department department = await departmentRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (department is null)
        {
            return Result<Department>.Failure("Department not found");
        }

        return Result<Department>.Succeed(department);
    }
}
