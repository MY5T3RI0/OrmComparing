using System.Diagnostics;

namespace OrmComparing.Comparing;

public class OperationComparer : IOrmComparer
{
    private readonly Stopwatch _stopwatch = new Stopwatch();

    protected async Task<CompareResult> CompareResultsAsync<T>(
        string[] operationName,
        Func<Task<T>>[] comparingFunctions)
    {
        if (operationName.Length != comparingFunctions.Length)
            throw new InvalidOperationException("Количество сравниваемых операций " +
                "должно быть равно их количеству");

        var i = 0;
        var exectionResults = new ExecutionResult[operationName.Length];

        foreach (var func in comparingFunctions)
        {
            var executionResult = new ExecutionResult(operationName[i++]);

            _stopwatch.Restart();

            await func();

            _stopwatch.Stop();

            exectionResults[i - 1] = executionResult;
        }

        return new(exectionResults);
    }
}
