using LinqToDB.Mapping;

namespace OrmComparing.Entities;

[Table(Schema = "dbo", Name = "Employees")]
public partial class Employees
{
    public Employees()
    {
        EmployeeTerritories = new HashSet<EmployeeTerritories>();
        InverseReportsToNavigation = new HashSet<Employees>();
        Orders = new HashSet<Orders>();
    }

    [PrimaryKey, Identity]
    public int EmployeeId { get; set; }
    
    [Column, NotNull]
    public string LastName { get; set; }

    [Column, NotNull]
    public string FirstName { get; set; }
    
    [Column, Nullable]
    public string Title { get; set; }
    
    [Column, Nullable]
    public string TitleOfCourtesy { get; set; }
    
    [Column, Nullable]
    public DateTime? BirthDate { get; set; }
    
    [Column, Nullable]
    public DateTime? HireDate { get; set; }
    
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
    public string HomePhone { get; set; }
    
    [Column, Nullable]
    public string Extension { get; set; }
    
    [Column, Nullable]
    public byte[] Photo { get; set; }
    
    [Column, Nullable]
    public string Notes { get; set; }
    
    [Column, Nullable]
    public int? ReportsTo { get; set; }
    
    [Column, Nullable]
    public string PhotoPath { get; set; }

    [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
    public ICollection<EmployeeTerritories> EmployeeTerritories { get; set; }

    [Association(ThisKey = "ReportsTo", OtherKey = "EmployeeID")]
    public Employees ReportsToNavigation { get; set; }

    [Association(ThisKey = "EmployeeID", OtherKey = "ReportsTo")]
    public ICollection<Employees> InverseReportsToNavigation { get; set; }

    [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
    public ICollection<Orders> Orders { get; set; }
}
