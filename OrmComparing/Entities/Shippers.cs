using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Shippers")]
public partial class Shippers
{
    public Shippers()
    {
        Orders = new HashSet<Orders>();
    }

    [PrimaryKey, Identity]
    public int ShipperId { get; set; }

    [Column, NotNull]
    public string CompanyName { get; set; }

    [Column, Nullable]
    public string Phone { get; set; }

    [Association(ThisKey = "ShipperID", OtherKey = "ShipVia")]
    public ICollection<Orders> Orders { get; set; }
}
