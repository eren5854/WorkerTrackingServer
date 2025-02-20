using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.DeleteDepartmentProductionById;
internal sealed class DeleteDepartmentProductionByIdCommandHandler(
    IDepartmentProductionRepository departmentProductionRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepartmentProductionByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDepartmentProductionByIdCommand request, CancellationToken cancellationToken)
    {
        DepartmentProduction departmentProduction = await departmentProductionRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (departmentProduction is null)
        {
            return Result<string>.Failure("Department production not found");
        }

        departmentProduction.IsDeleted = true;

        departmentProductionRepository.Update(departmentProduction);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Department production deleted successfully");
    }
}
