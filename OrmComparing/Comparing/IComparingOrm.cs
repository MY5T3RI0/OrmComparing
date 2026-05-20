namespace OrmComparing.Comparing;

public interface IComparingOrm
{
    string OrmName { get; set; }

    void ComplexTop500();
    void ComplexRawTop500();
    void SimpleTop500();
    void SimpleRawTop500();
    void SimpleTop10And10();
    void SimpleRawTop10And10();
    void ComplexTop10();
    void ComplexRawTop10();
    void SimpleTop10();
    void SimpleRawTop10();
}