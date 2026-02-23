using System.Reflection;

namespace OrmComparing.Comparing;

public class OrmComparer : OperationComparer
{
    private readonly List<IComparingOrm> _comparingRepos;
    public OrmComparer(List<IComparingOrm> comparingRepos)
    {
        _comparingRepos = comparingRepos;
    }

    public override List<CompareResult> CompareAllOperations()
    {
        var compareResults = new List<CompareResult>();

        var comparingMethodsInfos = typeof(IComparingOrm)
            .GetMethods(BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static)
            .Where(m => !m.IsSpecialName)
            .ToList();

        foreach (var comparingMethod in comparingMethodsInfos)
        {
            var comparingInfos = _comparingRepos
                .Select(r => 
                    {
                        // создаём делегат заранее
                        var del = (Action)Delegate.CreateDelegate(
                            typeof(Action),
                            r,
                            comparingMethod);

                        return new ComparingInfo(
                            del,
                            r.OrmName,
                            comparingMethod.Name);
                    })
                .ToArray();

            compareResults.Add(CompareResultsAsync(comparingInfos));
        }

        return compareResults;
    }
}
