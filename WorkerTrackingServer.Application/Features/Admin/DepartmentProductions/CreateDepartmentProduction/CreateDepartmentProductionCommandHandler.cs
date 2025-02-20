using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.CreateDepartmentProduction;
internal sealed class CreateDepartmentProductionCommandHandler(
    IDepartmentProductionRepository departmentProductionRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDepartmentProductionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateDepartmentProductionCommand request, CancellationToken cancellationToken)
    {
        bool isProductIdAndDepartmentIdExists = await departmentProductionRepository.AnyAsync(a => a.ProductId == request.ProductId && a.DepartmentId == request.DepartmentId, cancellationToken);

        if (isProductIdAndDepartmentIdExists)
        {
            return Result<string>.Failure("Aynı departman ve üründen kayıt mevcut");
        }

        DepartmentProduction departmentProduction = mapper.Map<DepartmentProduction>(request);
        departmentProduction.CreatedBy = "Admin";
        departmentProduction.CreatedDate = DateTime.Now;

        await departmentProductionRepository.AddAsync(departmentProduction, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Department production created successfully");
    }
}
