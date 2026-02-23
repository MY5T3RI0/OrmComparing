using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Order Details")]
public partial class OrderDetails
{
    [PrimaryKey(1), NotNull]
    public int? OrderId { get; set; }

    [PrimaryKey(2), NotNull]
    public int ProductId { get; set; }
    
    [Column, NotNull]
    public decimal UnitPrice { get; set; }
    
    [Column, NotNull]
    public short Quantity { get; set; }
    
    [Column, NotNull]
    public float Discount { get; set; }

    [Association(ThisKey = "OrderID", OtherKey = "OrderID")]
    public Orders Order { get; set; }

    [Association(ThisKey = "ProductID", OtherKey = "ProductID")]
    public Products Product { get; set; }
}
