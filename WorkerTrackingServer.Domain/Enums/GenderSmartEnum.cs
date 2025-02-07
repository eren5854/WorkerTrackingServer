using Ardalis.SmartEnum;

namespace WorkerTrackingServer.Domain.Enums;
public sealed class GenderSmartEnum : SmartEnum<GenderSmartEnum>
{
    public static readonly GenderSmartEnum Male = new GenderSmartEnum("Male", 1);
    public static readonly GenderSmartEnum Female = new GenderSmartEnum("Female", 2);
    public static readonly GenderSmartEnum Other = new GenderSmartEnum("Other", 3);

    public GenderSmartEnum(string name, int value) : base(name, value)
    {
    }
}
