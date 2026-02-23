using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Suppliers")]
public partial class Suppliers
{
    public Suppliers()
    {
        Products = new HashSet<Products>();
    }

    [PrimaryKey, Identity]
    public int SupplierId { get; set; }

    [Column, NotNull]
    public string CompanyName { get; set; }
    
    [Column, Nullable]
    public string ContactName { get; set; }
    
    [Column, Nullable]
    public string ContactTitle { get; set; }
    
    [Column, Nullable]
    public string Address { get; set; }
    
    [Column, Nullable]
    public string City { get; set; }
    
    [Column, Nullable]
    public string Region { get; set; }
    
    [Column, Nullable]
    public string PostalCode { get; set; }
    
    [Column, Nullable]
    public string Country { get; set; }
    
    [Column, Nullable]
    public string Phone { get; set; }
    
    [Column, Nullable]
    public string Fax { get; set; }
    
    [Column, Nullable]
    public string HomePage { get; set; }

    [Association(ThisKey = "SupplierID", OtherKey = "SupplierID")]
    public ICollection<Products> Products { get; set; }
}
