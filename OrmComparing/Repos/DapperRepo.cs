using Dapper;
using OrmComparing.Comparing;
using OrmComparing.DbContexts;
using OrmComparing.Entities;

namespace OrmComparing.Repos;

public class DapperRepo : IComparingOrm
{
    private readonly DapperContext _dapperContext;

    public DapperRepo(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public string OrmName { get; set; } = nameof(DapperContext);

    public void SimpleTop10()
        => SimpleRawTop10();

    public void SimpleTop500()
        => SimpleRawTop500();

    public void SimpleRawTop10()
    {
        using var connection = _dapperContext.CreateConnection();

        var sql = @"
SELECT TOP 10 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
						";
        var list = connection.Query<SimpleQueryRow>(sql).ToList();
    }

    public void SimpleRawTop500()
    {
        using var connection = _dapperContext.CreateConnection();

        var sql = @"
SELECT TOP 500 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
						";
        var list = connection.Query<SimpleQueryRow>(sql).ToList();
    }

    public void ComplexTop10()
        => ComplexRawTop10();

    public void ComplexTop500()
        => ComplexRawTop500();

    public void ComplexRawTop10()
    {
        var categoryIds = new[] { 68581, 68582, 68583, 68584, 68585, 68586, 68587, 68588, 68589, 68590 };
        var supplierIds = new[] { 34294, 34295, 34296, 34297, 34298, 34299, 34300, 34301, 34302, 34303, 34304, 34305, 34306, 34307, 34308, 34309, 34310, 34311, 34312, 34313, 34314, 34315, 34316, 34317, 34318, 34319, 34320, 34321, 34322 };

        using var connection = _dapperContext.CreateConnection();

        var sql = @"
SELECT TOP 10 OD.Quantity, OD.UnitPrice, OD.Discount, O.ShipCountry, S.Country
FROM Orders O
JOIN [Order Details] OD ON O.OrderID = OD.OrderID
JOIN Products P ON OD.ProductID = P.ProductID
JOIN Categories Cat ON P.CategoryID = Cat.CategoryID
JOIN Suppliers S ON P.SupplierID = S.SupplierID
WHERE
Cat.CategoryID IN @categoryIds
AND S.SupplierID IN @supplierIds
ORDER BY OD.Discount DESC
					";
        var list = connection.Query<ComplexQueryRow>(sql, new
        {
            categoryIds,
            supplierIds
        })
            .ToList();
    }

    public void ComplexRawTop500()
    {
        var categoryIds = new[] { 68581, 68582, 68583, 68584, 68585, 68586, 68587, 68588, 68589, 68590 };
        var supplierIds = new[] { 34294, 34295, 34296, 34297, 34298, 34299, 34300, 34301, 34302, 34303, 34304, 34305, 34306, 34307, 34308, 34309, 34310, 34311, 34312, 34313, 34314, 34315, 34316, 34317, 34318, 34319, 34320, 34321, 34322 };

        using var connection = _dapperContext.CreateConnection();

        var sql = @"
SELECT TOP 500 OD.Quantity, OD.UnitPrice, OD.Discount, O.ShipCountry, S.Country
FROM Orders O
JOIN [Order Details] OD ON O.OrderID = OD.OrderID
JOIN Products P ON OD.ProductID = P.ProductID
JOIN Categories Cat ON P.CategoryID = Cat.CategoryID
JOIN Suppliers S ON P.SupplierID = S.SupplierID
WHERE
Cat.CategoryID IN (@categoryIds)
AND S.SupplierID IN (@supplierIds)
ORDER BY OD.Discount DESC
					".Replace("@categoryIds", string.Join(",", categoryIds))
                .Replace("@supplierIds", string.Join(",", supplierIds));
        var list = connection.Query<ComplexQueryRow>(sql).ToList();
    }

    public void SimpleTop10And10()
        => SimpleRawTop10And10();

    public void SimpleRawTop10And10()
    {
        using var connection = _dapperContext.CreateConnection();

        var sql = @"
SELECT TOP 10 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
							";
        var list = connection.Query<SimpleQueryRow>(sql).ToList();

        sql = @"
SELECT TOP 10 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
							";
        var list2 = connection.Query<SimpleQueryRow>(sql).ToList();
    }
}
