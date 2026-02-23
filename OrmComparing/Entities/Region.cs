using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Region")]
public partial class Region
{
    public Region()
    {
        Territories = new HashSet<Territories>();
    }

    [PrimaryKey, NotNull]
    public int RegionId { get; set; }

    [Column, NotNull]
    public string RegionDescription { get; set; }

    [Association(ThisKey = "RegionID", OtherKey = "RegionID")]
    public ICollection<Territories> Territories { get; set; }
}
