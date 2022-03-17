using System.ComponentModel;

namespace Domain.Enums;
public enum TicketType
{
    [Description("Bug")]
    Bug = 1,

    [Description("Feature")]
    Feature = 2,

    [Description("Test")]
    Test = 3
}
