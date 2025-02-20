using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using WorkerTrackingServer.Domain.Departments;
using WorkerTrackingServer.Domain.Enums;

namespace WorkerTrackingServer.Domain.Users;
public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);

    public UserRoleSmartEnum Role { get; set; } = UserRoleSmartEnum.User;

    public string? WorkerCode { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public GenderSmartEnum? Gender { get; set; }

    public object DepartmentInfo => new
    {
        DepartmentId = DepartmentId,
        DepartmentName = Department?.DepartmentName,
        DepartmentDescription = Department?.DepartmentDescription
    };

    [JsonIgnore]
    public Guid? DepartmentId { get; set; }
    [JsonIgnore]
    public Department? Department { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }

    public int? ForgotPasswordCode { get; set; }
    public DateTime? ForgotPasswordCodeSendDate { get; set; }

    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
