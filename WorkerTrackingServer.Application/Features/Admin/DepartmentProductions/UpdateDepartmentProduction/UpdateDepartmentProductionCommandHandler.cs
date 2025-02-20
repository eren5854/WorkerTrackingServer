using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.UpdateDepartmentProduction;
internal sealed class UpdateDepartmentProductionCommandHandler(
    IDepartmentProductionRepository departmentProductionRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateDepartmentProductionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDepartmentProductionCommand request, CancellationToken cancellationToken)
    {
        bool isProductIdAndDepartmentIdExists = await departmentProductionRepository.AnyAsync(a => a.ProductId == request.ProductId && a.DepartmentId == request.DepartmentId, cancellationToken);

        if (isProductIdAndDepartmentIdExists)
        {
            return Result<string>.Failure("Aynı departman ve üründen kayıt mevcut");
        }


        DepartmentProduction departmentProduction = await departmentProductionRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (departmentProduction == null)
        {
            return Result<string>.Failure("Department production not found");
        }

        mapper.Map(request, departmentProduction);
        departmentProduction.UpdatedBy = "Admin";
        departmentProduction.UpdatedDate = DateTime.Now;

        departmentProductionRepository.Update(departmentProduction);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Department production update successfully");
    }
}
