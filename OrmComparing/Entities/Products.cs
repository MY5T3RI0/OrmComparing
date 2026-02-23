using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Products")]
public partial class Products
{
    public Products()
    {
        OrderDetails = new HashSet<OrderDetails>();
    }

    [PrimaryKey, Identity]
    public int ProductId { get; set; }

    [Column, NotNull]
    public string ProductName { get; set; }
    
    [Column, Nullable]
    public int? SupplierId { get; set; }
    
    [Column, Nullable]
    public int? CategoryId { get; set; }
    
    [Column, Nullable]
    public string QuantityPerUnit { get; set; }
    
    [Column, Nullable]
    public decimal? UnitPrice { get; set; }
    
    [Column, Nullable]
    public short? UnitsInStock { get; set; }
    
    [Column, Nullable]
    public short? UnitsOnOrder { get; set; }
    
    [Column, Nullable]
    public short? ReorderLevel { get; set; }

    [Column, NotNull]
    public bool? Discontinued { get; set; }

    [Association(ThisKey = "CategoryID", OtherKey = "CategoryID")]
    public Categories Category { get; set; }

    [Association(ThisKey = "SupplierID", OtherKey = "SupplierID")]
    public Suppliers Supplier { get; set; }

    [Association(ThisKey = "ProductID", OtherKey = "ProductID")]
    public ICollection<OrderDetails> OrderDetails { get; set; }
}
