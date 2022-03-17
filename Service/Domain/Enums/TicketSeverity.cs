using System.ComponentModel;

namespace Domain.Enums;

public enum TicketSeverity
{
    [Description("Information")]
    Information = 0,

    [Description("Warning")]
    Warning = 1,

    [Description("Error")]
    Error = 2,

    [Description("Critical")]
    Critical = 3
}