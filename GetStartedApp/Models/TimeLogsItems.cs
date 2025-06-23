using System.Collections.Generic;

namespace GetStartedApp.Models;

public class TLTimeLogItem
{
    public required TimeLog TimeLog { get; init; }
    public required string EmployeeName { get; init; }
    public required string Color { get; init; }
}

public class TLTaskItem
{
    public required Task Task { get; init; }
    public required List<TLTimeLogItem> TimeLogItems { get; set; }
}

public class TLProjectItem
{
    public required Project Project { get; init; }
    public required List<TLTaskItem> TaskItems { get; set; }
}
