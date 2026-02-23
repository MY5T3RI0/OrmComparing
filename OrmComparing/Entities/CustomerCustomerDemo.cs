using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "CustomerCustomerDemo")]
public partial class CustomerCustomerDemo
{
    [PrimaryKey(1), NotNull]
    public string CustomerId { get; set; }

    [PrimaryKey(2), NotNull]
    public string CustomerTypeId { get; set; }

    [Association(ThisKey = "CustomerID", OtherKey = "CustomerID")]
    public Customers Customer { get; set; }

    [Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID")]
    public CustomerDemographics CustomerType { get; set; }
}
