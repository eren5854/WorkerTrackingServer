using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetAllDepartmentProduction;
internal sealed class GetAllDepartmentProductionQueryHandler(
    IDepartmentProductionRepository departmentProductionRepository) : IRequestHandler<GetAllDepartmentProductionQuery, Result<List<DepartmentProduction>>>
{
    public async Task<Result<List<DepartmentProduction>>> Handle(GetAllDepartmentProductionQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentProduction> departmentProductions = await departmentProductionRepository.GetAll().Include(i => i.Department).Include(i => i.Product).OrderBy(o => o.CreatedDate).ToListAsync(cancellationToken);

        return Result<List<DepartmentProduction>>.Succeed(departmentProductions);
    }
}
