using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.UpdateDepartment;
internal sealed class UpdateDepartmentCommandHandler(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateDepartmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        Department department = await departmentRepository.GetByExpressionAsync(g => g.Id == request.Id);
        if (department is null)
        {
            return Result<string>.Failure("Department not found");
        }

        mapper.Map(request, department);
        department.UpdatedDate = DateTime.Now;
        department.UpdatedBy = "Admin";

        departmentRepository.Update(department);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Department update successfully");
    }
}
