using SQLite;

namespace ben.Models;

public enum Status
{
    NotStarted = 0,
    InProgress = 1,
    Forwarded = 2,
    Complete = 3
}

public class TodoItem
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }

    // Persisted enum status (stored as integer by sqlite-net)
    public Status Status { get; set; }

    // Backwards-compatible boolean for existing bindings/views.
    // Ignored by SQLite mapping since we persist the enum instead.
    [Ignore]
    public bool Done
    {
        get => Status == Status.Complete;
        set => Status = value ? Status.Complete : Status.NotStarted;
    }
}
