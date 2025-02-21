using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetDepartmentProductionById;
internal sealed class GetDepartmentProductionByIdCommandHandler(
    IDepartmentProductionRepository departmentProductionRepository) : IRequestHandler<GetDepartmentProductionByIdCommand, Result<DepartmentProduction>>
{
    public async Task<Result<DepartmentProduction>> Handle(GetDepartmentProductionByIdCommand request, CancellationToken cancellationToken)
    {
        DepartmentProduction? departmentProduction = await departmentProductionRepository.Where(g => g.Id == request.Id).Include(i => i.Department).Include(i => i.Product).FirstOrDefaultAsync(cancellationToken);
        if (departmentProduction is null)
        {
            return Result<DepartmentProduction>.Failure("Department production not found");
        }

        return Result<DepartmentProduction>.Succeed(departmentProduction);
    }
}
