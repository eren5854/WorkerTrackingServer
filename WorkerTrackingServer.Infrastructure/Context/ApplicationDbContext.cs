using ED.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.EmailSettings;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Users;
using WorkerTrackingServer.Domain.WorkerAssignments;
using WorkerTrackingServer.Domain.WorkerProductions;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Infrastructure.Context;
public sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentProduction> DepartmentProductions { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<WorkerAssignment> WorkerAssignments { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<WorkerLogin> WorkerLogins { get; set; }
    public DbSet<WorkerProduction> WorkerProductions { get; set; }
    public DbSet<WorkerDailyProduction> WorkerDailyProductions { get; set; }
    public DbSet<WorkerWeeklyProduction> WorkerWeeklyProductions { get; set; }
    public DbSet<WorkerMonthlyProduction> WorkerMonthlyProductions { get; set; }
    public DbSet<WorkerYearlyProduction> WorkerYearlyProductions { get; set; }
    public DbSet<EmailSetting> EmailSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<DepartmentProduction>()
        //    .HasOne(x => x.Department)
        //    .WithMany(x => x.DepartmentProductions)
        //    .HasForeignKey(x => x.DepartmentId)
        //    .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<WorkerAssignment>()
            .HasOne(x => x.WorkerProduction)
            .WithMany(x => x.WorkerAssignments)
            .HasForeignKey(x => x.WorkerProductionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<WorkerAssignment>()
            .HasOne(x => x.Machine)
            .WithMany(x => x.WorkerAssignments)
            .HasForeignKey(x => x.MachineId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<WorkerAssignment>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.WorkerAssignments) // WithOne yerine WithMany()
            .HasForeignKey(x => x.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Entity<WorkerProduction>()
            .HasOne(x => x.AppUser)
            .WithMany(x => x.WorkerProductions)
            .HasForeignKey(x => x.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<WorkerProduction>()
            .HasOne(x => x.Product)
            .WithMany(x => x.WorkerProductions)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
