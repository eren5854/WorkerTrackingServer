﻿using ED.GenericRepository;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Domain.Repositories;
public interface IDepartmentRepository : IRepository<Department>
{
    //public Task<bool> FindDepartmentName(string name, CancellationToken cancellationToken);
} 
