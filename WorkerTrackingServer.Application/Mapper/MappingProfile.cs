using AutoMapper;
using WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.CreateDepartmentProduction;
using WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.UpdateDepartmentProduction;
using WorkerTrackingServer.Application.Features.Admin.Departments.CreateDepartment;
using WorkerTrackingServer.Application.Features.Admin.Departments.UpdateDepartment;
using WorkerTrackingServer.Application.Features.Admin.EmailSettings.CreateEmailSetting;
using WorkerTrackingServer.Application.Features.Admin.EmailSettings.UpdateEmailSetting;
using WorkerTrackingServer.Application.Features.Admin.Machines.CreateMachine;
using WorkerTrackingServer.Application.Features.Admin.Machines.UpdateMachine;
using WorkerTrackingServer.Application.Features.Admin.Products.CreateProduct;
using WorkerTrackingServer.Application.Features.Admin.Products.UpdateProduct;
using WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorker;
using WorkerTrackingServer.Application.Features.Admin.Workers.WorkerRegister;
using WorkerTrackingServer.Application.Features.Auth.RegisterAdmin;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.EmailSettings;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Products;
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

        CreateMap<CreateDepartmentProductionCommand, DepartmentProduction>();
        CreateMap<UpdateDepartmentProductionCommand, DepartmentProduction>();

        CreateMap<CreateEmailSettingCommand, EmailSetting>();
        CreateMap<UpdateEmailSettingCommand, EmailSetting>();

        CreateMap<CreateMachineCommand, Machine>();
        CreateMap<UpdateMachineCommand, Machine>();

        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();


    }
}
