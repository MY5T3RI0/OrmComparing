namespace OrmComparing.Comparing;

public class ExecutionResult
{
    public ExecutionResult(string connectionName, TimeSpan time)
    {
        ConnectionName = connectionName;
        Time = time;
    }
    public ExecutionResult(string connectionName)
    {
        ConnectionName = connectionName;
    }

    public string ConnectionName { get; set; } = null!;
    public TimeSpan Time { get; set; }
}