using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.CreateDepartment;
internal sealed class CreateDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDepartmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var isDepartmentExists = await departmentRepository.AnyAsync(a => a.DepartmentName == request.DepartmentName);
        if (isDepartmentExists)
        {
            return Result<string>.Failure("Department already exists!");
        }

        Department department = mapper.Map<Department>(request);
        department.CreatedBy = "Admin";
        department.CreatedDate = DateTime.Now;
        await departmentRepository.AddAsync(department, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Department created successfully");



    }
}
