using System;

namespace GetStartedApp.Models;

public class TimeLog
{
    public int TimeLogId { get; set; }

    public int TaskId { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan TimeSpent { get; set; }

    public sbyte IsApproved { get; set; }

    public virtual Task Task { get; set; } = null!;
}