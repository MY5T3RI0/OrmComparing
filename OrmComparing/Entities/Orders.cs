using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Orders")]
public partial class Orders
{
    public Orders()
    {
        OrderDetails = new HashSet<OrderDetails>();
    }

    [PrimaryKey, Identity]
    public int OrderId { get; set; }
    
    [Column, Nullable]
    public string CustomerId { get; set; }

    [Column, Nullable]
    public int? EmployeeId { get; set; }
    
    [Column, Nullable]
    public DateTime? OrderDate { get; set; }
    
    [Column, Nullable]
    public DateTime? RequiredDate { get; set; }
    
    [Column, Nullable]
    public DateTime? ShippedDate { get; set; }
    
    [Column, Nullable]
    public int? ShipVia { get; set; }
    
    [Column, Nullable]
    public decimal? Freight { get; set; }
    
    [Column, Nullable]
    public string ShipName { get; set; }
    
    [Column, Nullable]
    public string ShipAddress { get; set; }
    
    [Column, Nullable]
    public string ShipCity { get; set; }
    
    [Column, Nullable]
    public string ShipRegion { get; set; }
    
    [Column, Nullable]
    public string ShipPostalCode { get; set; }
    
    [Column, Nullable]
    public string ShipCountry { get; set; }

    [Association(ThisKey = "CustomerID", OtherKey = "CustomerID")]
    public Customers Customer { get; set; }

    [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
    public Employees Employee { get; set; }

    [Association(ThisKey = "ShipVia", OtherKey = "ShipperID")]
    public Shippers ShipViaNavigation { get; set; }

    [Association(ThisKey = "OrderID", OtherKey = "OrderID")]
    public ICollection<OrderDetails> OrderDetails { get; set; }
}
