using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Categories")]
public partial class Categories
{
    public Categories()
    {
        Products = new HashSet<Products>();
    }

    [PrimaryKey, Identity]
    public int CategoryId { get; set; }

    [Column, NotNull]
    public string CategoryName { get; set; }

    [Column, Nullable]
    public string Description { get; set; }

    [Column, Nullable]
    public byte[] Picture { get; set; }

    [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = true)]
    public ICollection<Products> Products { get; set; }
}
