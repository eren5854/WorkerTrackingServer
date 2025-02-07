using Ardalis.SmartEnum;

namespace WorkerTrackingServer.Domain.Enums;
public sealed class UserRoleSmartEnum : SmartEnum<UserRoleSmartEnum>
{
    public static readonly UserRoleSmartEnum MasterAdmin = new UserRoleSmartEnum("MasterAdmin", 1);
    public static readonly UserRoleSmartEnum Admin = new UserRoleSmartEnum("Admin", 2);
    public static readonly UserRoleSmartEnum User = new UserRoleSmartEnum("User", 3);
    public UserRoleSmartEnum(string name, int value) : base(name, value)
    {
    }
}
