using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "EmployeeTerritories")]
public partial class EmployeeTerritories
{
    [PrimaryKey(1), NotNull]
    public int EmployeeId { get; set; }

    [PrimaryKey(2), NotNull]
    public string TerritoryId { get; set; }

    [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
    public Employees Employee { get; set; }

    [Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID")]
    public Territories Territory { get; set; }
}
