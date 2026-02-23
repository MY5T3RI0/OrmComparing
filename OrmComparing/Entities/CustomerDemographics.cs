using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "CustomerDemographics")]
public partial class CustomerDemographics
{
    public CustomerDemographics()
    {
        CustomerCustomerDemo = new HashSet<CustomerCustomerDemo>();
    }

    [PrimaryKey, NotNull]
    public string CustomerTypeId { get; set; }

    [Column, Nullable]
    public string CustomerDesc { get; set; }

    [Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID")]
    public ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
}
