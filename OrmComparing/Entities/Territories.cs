using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Territories")]
public partial class Territories
{
    public Territories()
    {
        EmployeeTerritories = new HashSet<EmployeeTerritories>();
    }

    [PrimaryKey, NotNull]
    public string TerritoryId { get; set; }
    
    [Column, NotNull]
    public string TerritoryDescription { get; set; }
    
    [Column, NotNull]
    public int? RegionId { get; set; }

    [Association(ThisKey = "RegionID", OtherKey = "RegionID")]
    public Region? Region { get; set; }

    [Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID")]
    public ICollection<EmployeeTerritories> EmployeeTerritories { get; set; }
}
