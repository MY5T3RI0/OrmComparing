using System.Diagnostics;
using System.Reflection;

namespace OrmComparing.Comparing;

public class OperationComparer
{
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public virtual List<CompareResult> CompareAllOperations()
    {
        var compareResults = new List<CompareResult>();

        var comparingMethodsInfos = GetType().GetMethods().Where(m => m.GetCustomAttribute(typeof(ComparingOperationAttribute), true) != null);

        foreach (var comparingMethodsInfo in comparingMethodsInfos)
        {
            var compareResult = comparingMethodsInfo?.Invoke(this, null) as CompareResult;

            if (compareResult != null)
                compareResults.Add(compareResult);
        }

        return compareResults;
    }

    protected virtual CompareResult CompareResultsAsync(
        ComparingInfo[] comparingInfos)
    {
        var i = 0;
        var exectionResults = new ExecutionResult[comparingInfos.Length];

        foreach (var comparingInfo in comparingInfos)
        {
            var executionResult = new ExecutionResult(comparingInfo.ComparingTypeName, comparingInfo.OperationName);

            var time = new TimeSpan();

            for(var j = 0; j < 5; j++)
                comparingInfo.Operation(); // Для разогрева

            for (var j = 0; j < 1000; j++)
            {
                _stopwatch.Restart();

                comparingInfo.Operation();

                _stopwatch.Stop();

                time += _stopwatch.Elapsed;
            }

            executionResult.Time = time / 1000;

            exectionResults[i++] = executionResult;
        }

        return new(exectionResults);
    }
}
