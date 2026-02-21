namespace OrmComparing.Comparing;

public class OrmComparer<T> : OperationComparer
{
    private readonly List<IComparingRepository<T>> _comparingRepos;
    public OrmComparer(List<IComparingRepository<T>> comparingRepos)
    {
        _comparingRepos = comparingRepos;
    }

    public async Task<CompareResult> CompareGetTop10Async()
    {
        return await CompareResultsAsync(
            _comparingRepos.Select(x => x.OrmName).ToArray(),
            _comparingRepos.Select<IComparingRepository<T>, Func<Task<List<T>>>>(x => x.GetTop10Async).ToArray());
    }
}
