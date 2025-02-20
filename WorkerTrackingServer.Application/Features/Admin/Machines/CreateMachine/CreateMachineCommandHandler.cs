using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.CreateMachine;
internal sealed class CreateMachineCommandHandler(
    IMachineRepository machineRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateMachineCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateMachineCommand request, CancellationToken cancellationToken)
    {
        bool isMachineNumberIsExists = await machineRepository.AnyAsync(a => a.MachineNumber == request.MachineNumber, cancellationToken);
        if (isMachineNumberIsExists)
        {
            return Result<string>.Failure("Machine Number already exists");
        }

        bool isMachineNameIsExists = await machineRepository.AnyAsync(a => a.MachineName == request.MachineName, cancellationToken);
        if (isMachineNameIsExists)
        {
            return Result<string>.Failure("Machine Name already exists");
        }

        Machine machine = mapper.Map<Machine>(request);
        machine.CreatedDate = DateTime.Now;
        machine.CreatedBy = "Admin";
        machine.MachineStatus = true;

        await machineRepository.AddAsync(machine,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Machine created successfully");
    }
}
