namespace OrmComparing.Comparing;

public interface IComparingRepository<T>
{
    string OrmName { get; set; }
    Task<List<T>> GetTop10Async();
}