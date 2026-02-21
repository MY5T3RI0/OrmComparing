using LinqToDB.Mapping;
using NpgsqlTypes;

namespace OrmComparing.Entities;

[Table("Building")]
public class Building
{
    [PrimaryKey]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("Amenity")]
    public string Amenity { get; set; }

    [Column("Coordinates")]
    public NpgsqlPoint Coordinates { get; set; }
}
