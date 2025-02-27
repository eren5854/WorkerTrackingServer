using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetAllDepartmentProductionByDepartmentId;
internal sealed class GetAllDepartmentProductionByDepartmentIdCommandHandler(
    IDepartmentProductionRepository departmentProductionRepository) : IRequestHandler<GetAllDepartmentProductionByDepartmentIdCommand, Result<List<DepartmentProduction>>>
{
    public async Task<Result<List<DepartmentProduction>>> Handle(GetAllDepartmentProductionByDepartmentIdCommand request, CancellationToken cancellationToken)
    {
        List<DepartmentProduction> departmentProductions = await departmentProductionRepository.GetAll().Where(w => w.DepartmentId == request.Id).Include(i => i.Department).Include(i => i.Product).OrderByDescending(o => o.CreatedDate).ToListAsync(cancellationToken);

        return Result<List<DepartmentProduction>>.Succeed(departmentProductions);
    }
}
