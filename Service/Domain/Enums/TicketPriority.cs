using System.ComponentModel;

namespace Domain.Enums;

public enum TicketPriority
{
    [Description("Low")]
    Low = 0,

    [Description("Medium")]
    Medium = 1,

    [Description("Major")]
    Major = 2,

    [Description("Critical")]
    Critical = 3
}