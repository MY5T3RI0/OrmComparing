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

        foreach (var executionTime in ExecutionResults)
            sb.Append($"Время выполнения запроса при использовании {executionTime.ConnectionName} " +
                $"составило {executionTime.Time}{Environment.NewLine}");

        return sb.ToString();
    }
}