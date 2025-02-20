using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.GetAllDepartment;
internal sealed class GetAllDepartmentQueryHandler(
    IDepartmentRepository departmentRepository) : IRequestHandler<GetAllDepartmentQuery, Result<List<Department>>>
{
    public async Task<Result<List<Department>>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
    {
        List<Department> departments = await departmentRepository.GetAll().ToListAsync(cancellationToken);
        return Result<List<Department>>.Succeed(departments);
    }
}
