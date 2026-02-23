using LinqToDB.Data;
using OrmComparing.Comparing;
using OrmComparing.DbContexts;
using OrmComparing.Entities;

namespace OrmComparing.Repos;

public class Linq2dbRepo : IComparingOrm
{
    private readonly Linq2dbConnection db;

    public string OrmName { get; set; } = nameof(Linq2dbConnection);

    public Linq2dbRepo(Linq2dbConnection connection)
    {
        db = connection;
    }

    public void SimpleTop10()
    {
        var list =
            (
                from o in db.Orders
                join c in db.Customers on o.CustomerId equals c.CustomerId
                select new { o.OrderId, o.OrderDate, c.Country, c.CompanyName }
            ).Take(10).ToList();
    }

    public void SimpleTop500()
    {
        var list =
            (
                from o in db.Orders
                join c in db.Customers on o.CustomerId equals c.CustomerId
                select new { o.OrderId, o.OrderDate, c.Country, c.CompanyName }
            ).Take(500).ToList();
    }

    public void SimpleRawTop10()
    {
        var sql = @"
SELECT TOP 10 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
						";
        var list = db.Query<SimpleQueryRow>(sql).ToList();
    }

    public void SimpleRawTop500()
    {
        var sql = @"
SELECT TOP 500 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
						";
        var list = db.Query<SimpleQueryRow>(sql).ToList();
    }

    public void ComplexTop10()
    {
        var categoryIds = new[] { 68581, 68582, 68583, 68584, 68585, 68586, 68587, 68588, 68589, 68590 };
        var supplierIds = new[] { 34294, 34295, 34296, 34297, 34298, 34299, 34300, 34301, 34302, 34303, 34304, 34305, 34306, 34307, 34308, 34309, 34310, 34311, 34312, 34313, 34314, 34315, 34316, 34317, 34318, 34319, 34320, 34321, 34322 };

        var list =
            (
                from o in db.Orders
                join od in db.OrderDetails on o.OrderId equals od.OrderId
                join p in db.Products on od.ProductId equals p.ProductId
                join cat in db.Categories on p.CategoryId equals cat.CategoryId
                join s in db.Suppliers on p.SupplierId equals s.SupplierId
                where categoryIds.Contains(cat.CategoryId)
                    && supplierIds.Contains(s.SupplierId)
                orderby od.Discount descending
                select new { od.Quantity, od.UnitPrice, od.Discount, o.ShipCountry, s.Country }
            ).Take(10).ToList();
    }

    public void ComplexTop500()
    {
        var categoryIds = new[] { 68581, 68582, 68583, 68584, 68585, 68586, 68587, 68588, 68589, 68590 };
        var supplierIds = new[] { 34294, 34295, 34296, 34297, 34298, 34299, 34300, 34301, 34302, 34303, 34304, 34305, 34306, 34307, 34308, 34309, 34310, 34311, 34312, 34313, 34314, 34315, 34316, 34317, 34318, 34319, 34320, 34321, 34322 };

        var list =
            (
                from o in db.Orders
                join od in db.OrderDetails on o.OrderId equals od.OrderId
                join p in db.Products on od.ProductId equals p.ProductId
                join cat in db.Categories on p.CategoryId equals cat.CategoryId
                join s in db.Suppliers on p.SupplierId equals s.SupplierId
                where categoryIds.Contains(cat.CategoryId)
                    && supplierIds.Contains(s.SupplierId)
                orderby od.Discount descending
                select new { od.Quantity, od.UnitPrice, od.Discount, o.ShipCountry, s.Country }
            ).Take(500).ToList();
    }

    public void ComplexRawTop10()
    {
        var categoryIds = new[] { 68581, 68582, 68583, 68584, 68585, 68586, 68587, 68588, 68589, 68590 };
        var supplierIds = new[] { 34294, 34295, 34296, 34297, 34298, 34299, 34300, 34301, 34302, 34303, 34304, 34305, 34306, 34307, 34308, 34309, 34310, 34311, 34312, 34313, 34314, 34315, 34316, 34317, 34318, 34319, 34320, 34321, 34322 };

        var sql = @"
SELECT TOP 10 OD.Quantity, OD.UnitPrice, OD.Discount, O.ShipCountry, S.Country
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
        var list = db.Query<ComplexQueryRow>(sql).ToList();
    }

    public void ComplexRawTop500()
    {
        var categoryIds = new[] { 68581, 68582, 68583, 68584, 68585, 68586, 68587, 68588, 68589, 68590 };
        var supplierIds = new[] { 34294, 34295, 34296, 34297, 34298, 34299, 34300, 34301, 34302, 34303, 34304, 34305, 34306, 34307, 34308, 34309, 34310, 34311, 34312, 34313, 34314, 34315, 34316, 34317, 34318, 34319, 34320, 34321, 34322 };

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
        var list = db.Query<ComplexQueryRow>(sql).ToList();
    }

    public void SimpleTop10And10()
    {
        var list =
            (
                from o in db.Orders
                join c in db.Customers on o.CustomerId equals c.CustomerId
                select new { o.OrderId, o.OrderDate, c.Country, c.CompanyName }
            ).Take(10).ToList();
        var list2 =
            (
                from o in db.Orders
                join c in db.Customers on o.CustomerId equals c.CustomerId
                select new { o.OrderId, o.OrderDate, c.Country, c.CompanyName }
            ).Take(10).ToList();
    }

    public void SimpleRawTop10And10()
    {
        var sql = @"
SELECT TOP 10 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
							";
        var list = db.Query<SimpleQueryRow>(sql).ToList();

        sql = @"
SELECT TOP 10 O.OrderID, O.OrderDate, C.Country, C.CompanyName
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
							";
        var list2 = db.Query<SimpleQueryRow>(sql).ToList();
    }
}
