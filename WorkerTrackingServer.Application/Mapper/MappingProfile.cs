using AutoMapper;
using WorkerTrackingServer.Application.Features.Admin.Departments.CreateDepartment;
using WorkerTrackingServer.Application.Features.Admin.Departments.UpdateDepartment;
using WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorker;
using WorkerTrackingServer.Application.Features.Admin.Workers.WorkerRegister;
using WorkerTrackingServer.Application.Features.Auth.RegisterAdmin;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Mapper;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterAdminCommand, AppUser>();
        CreateMap<WorkerRegisterCommand, AppUser>();
        CreateMap<UpdateWorkerCommand, AppUser>();

        CreateMap<CreateDepartmentCommand, Department>();
        CreateMap<UpdateDepartmentCommand, Department>();
    }
}
