using Microsoft.AspNetCore.Identity;
using WorkerTrackingServer.Domain.Enums;

namespace WorkerTrackingServer.Domain.Users;
public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);

    public UserRoleSmartEnum Role { get; set; } = UserRoleSmartEnum.User;

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }

    public int? ForgotPasswordCode { get; set; }
    public DateTime? ForgotPasswordCodeSendDate { get; set; }

    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
