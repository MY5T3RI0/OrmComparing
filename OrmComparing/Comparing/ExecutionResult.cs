namespace OrmComparing.Comparing;

public class ExecutionResult
{
    public ExecutionResult(string comparingTypeName, string operationName, TimeSpan time)
    {
        ComparingTypeName = comparingTypeName;
        OperationName = operationName;
        Time = time;
    }
    public ExecutionResult(string connectionName, string operationName)
    {
        ComparingTypeName = connectionName;
        OperationName = operationName;
    }

    public string ComparingTypeName { get; set; } = null!;
    public string OperationName { get; set; } = null!;
    public TimeSpan Time { get; set; }
}