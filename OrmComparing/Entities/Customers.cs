using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Customers")]
public partial class Customers
{
    public Customers()
    {
        CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
        Orders = new HashSet<Orders>();
    }

    [PrimaryKey, NotNull]
    public string CustomerId { get; set; }

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

    [Association(ThisKey = "CustomerID", OtherKey = "CustomerID")]
    public ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }

    [Association(ThisKey = "CustomerID", OtherKey = "CustomerID")]
    public ICollection<Orders> Orders { get; set; }
}
