using System.Text;

namespace OrmComparing.Comparing;

public class CompareResult
{
    public CompareResult(params ExecutionResult[] executionTimes)
    {
        ExecutionResults.AddRange(executionTimes);
    }

    public List<ExecutionResult> ExecutionResults { get; set; } = new();

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var executionResult in ExecutionResults)
            sb.Append($"Время выполнения запроса {executionResult.OperationName} при использовании {executionResult.ComparingTypeName} " +
                $"составило {executionResult.Time}{Environment.NewLine}");

        return sb.ToString();
    }
}