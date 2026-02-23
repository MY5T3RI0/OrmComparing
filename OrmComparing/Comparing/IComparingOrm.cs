namespace OrmComparing.Comparing;

public interface IComparingOrm
{
    string OrmName { get; set; }

    void ComplexRawTop10();
    void ComplexRawTop500();
    void ComplexTop10();
    void ComplexTop500();
    void SimpleRawTop10();
    void SimpleRawTop10And10();
    void SimpleRawTop500();
    void SimpleTop10();
    void SimpleTop10And10();
    void SimpleTop500();
}