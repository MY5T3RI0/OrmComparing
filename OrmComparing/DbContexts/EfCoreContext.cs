using Microsoft.EntityFrameworkCore;
using OrmComparing.Entities;

namespace OrmComparing.DbContexts;

public partial class EfCoreContext : DbContext
{
    public EfCoreContext()
    {
        
    }

    public EfCoreContext(DbContextOptions<EfCoreContext> options)
        : base(options) { }

    public virtual DbSet<Categories> Categories { get; set; }
    public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }
    public virtual DbSet<Customers> Customers { get; set; }
    public virtual DbSet<Employees> Employees { get; set; }
    public virtual DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }
    public virtual DbSet<OrderDetails> OrderDetails { get; set; }
    public virtual DbSet<Orders> Orders { get; set; }
    public virtual DbSet<Products> Products { get; set; }
    public virtual DbSet<Region> Region { get; set; }
    public virtual DbSet<Shippers> Shippers { get; set; }
    public virtual DbSet<Suppliers> Suppliers { get; set; }
    public virtual DbSet<Territories> Territories { get; set; }
    public virtual DbSet<SimpleQueryRow> SimpleQueryRows { get; set; }
    public virtual DbSet<ComplexQueryRow> ComplexQueryRows { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(@"Data Source=localhost,1433;Initial Catalog=TestDb;User ID=sa;Password=!Superadmin3000;TrustServerCertificate=true");
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SimpleQueryRow>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate).HasColumnName("OrderDate");
            entity.Property(e => e.Country).HasColumnName("Country");
            entity.Property(e => e.CompanyName).HasColumnName("CompanyName");
        });

        modelBuilder.Entity<ComplexQueryRow>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.Quantity).HasColumnName("Quantity");
            entity.Property(e => e.UnitPrice).HasColumnName("UnitPrice");
            entity.Property(e => e.Discount).HasColumnName("Discount");
            entity.Property(e => e.ShipCountry).HasColumnName("ShipCountry");
            entity.Property(e => e.Country).HasColumnName("Country");
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.HasIndex(e => e.CategoryName)
                .HasDatabaseName("CategoryName");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            entity.Property(e => e.Description).HasColumnType("ntext");

            entity.Property(e => e.Picture).HasColumnType("image");
        });

        modelBuilder.Entity<CustomerCustomerDemo>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId });

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasColumnType("nvarchar(15)");

            entity.Property(e => e.CustomerTypeId)
                .HasColumnName("CustomerTypeID")
                .HasColumnType("nvarchar(15)");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerCustomerDemo)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCustomerDemo_Customers");

            entity.HasOne(d => d.CustomerType)
                .WithMany(p => p.CustomerCustomerDemo)
                .HasForeignKey(d => d.CustomerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCustomerDemo");
        });

        modelBuilder.Entity<CustomerDemographics>(entity =>
        {
            entity.HasKey(e => e.CustomerTypeId);

            entity.Property(e => e.CustomerTypeId)
                .HasColumnName("CustomerTypeID")
                .HasColumnType("nvarchar(15)")
                .ValueGeneratedNever();

            entity.Property(e => e.CustomerDesc).HasColumnType("ntext");
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.HasIndex(e => e.City)
                .HasDatabaseName("City");

            entity.HasIndex(e => e.CompanyName)
                .HasDatabaseName("CompanyName");

            entity.HasIndex(e => e.PostalCode)
                .HasDatabaseName("PostalCode");

            entity.HasIndex(e => e.Region)
                .HasDatabaseName("Region");

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasColumnType("nvarchar(15)")
                .ValueGeneratedNever();

            entity.Property(e => e.Address).HasMaxLength(60);

            entity.Property(e => e.City).HasMaxLength(15);

            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.ContactName).HasMaxLength(30);

            entity.Property(e => e.ContactTitle).HasMaxLength(30);

            entity.Property(e => e.Country).HasMaxLength(15);

            entity.Property(e => e.Fax).HasMaxLength(24);

            entity.Property(e => e.Phone).HasMaxLength(24);

            entity.Property(e => e.PostalCode).HasMaxLength(15);

            entity.Property(e => e.Region).HasMaxLength(15);
        });

        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.HasIndex(e => e.LastName)
                .HasDatabaseName("LastName");

            entity.HasIndex(e => e.PostalCode)
                .HasDatabaseName("PostalCode");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.Property(e => e.Address).HasMaxLength(60);

            entity.Property(e => e.BirthDate).HasColumnType("datetime");

            entity.Property(e => e.City).HasMaxLength(15);

            entity.Property(e => e.Country).HasMaxLength(15);

            entity.Property(e => e.Extension).HasMaxLength(15);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(15);

            entity.Property(e => e.HireDate).HasColumnType("datetime");

            entity.Property(e => e.HomePhone).HasMaxLength(24);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Notes).HasColumnType("ntext");

            entity.Property(e => e.Photo).HasColumnType("image");

            entity.Property(e => e.PhotoPath).HasMaxLength(255);

            entity.Property(e => e.PostalCode).HasMaxLength(15);

            entity.Property(e => e.Region).HasMaxLength(15);

            entity.Property(e => e.Title).HasMaxLength(30);

            entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

            entity.HasOne(d => d.ReportsToNavigation)
                .WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("FK_Employees_Employees");
        });

        modelBuilder.Entity<EmployeeTerritories>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.TerritoryId });

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasMaxLength(20);

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeTerritories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTerritories_Employees");

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.EmployeeTerritories)
                .HasForeignKey(d => d.TerritoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTerritories_Territories");
        });

        modelBuilder.Entity<OrderDetails>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("Order Details");

            entity.HasIndex(e => e.OrderId)
                .HasDatabaseName("OrdersOrder_Details");

            entity.HasIndex(e => e.ProductId)
                .HasDatabaseName("ProductsOrder_Details");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.Discount).HasDefaultValueSql("(0)");

            entity.Property(e => e.Quantity).HasDefaultValueSql("(1)");

            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasDefaultValueSql("(0)");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.HasIndex(e => e.CustomerId)
                .HasDatabaseName("CustomersOrders");

            entity.HasIndex(e => e.EmployeeId)
                .HasDatabaseName("EmployeesOrders");

            entity.HasIndex(e => e.OrderDate)
                .HasDatabaseName("OrderDate");

            entity.HasIndex(e => e.ShipPostalCode)
                .HasDatabaseName("ShipPostalCode");

            entity.HasIndex(e => e.ShipVia)
                .HasDatabaseName("ShippersOrders");

            entity.HasIndex(e => e.ShippedDate)
                .HasDatabaseName("ShippedDate");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasColumnType("nvarchar(15)");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.Property(e => e.Freight)
                .HasColumnType("money")
                .HasDefaultValueSql("(0)");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.Property(e => e.RequiredDate).HasColumnType("datetime");

            entity.Property(e => e.ShipAddress).HasMaxLength(60);

            entity.Property(e => e.ShipCity).HasMaxLength(15);

            entity.Property(e => e.ShipCountry).HasMaxLength(15);

            entity.Property(e => e.ShipName).HasMaxLength(40);

            entity.Property(e => e.ShipPostalCode).HasMaxLength(15);

            entity.Property(e => e.ShipRegion).HasMaxLength(15);

            entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.ShipViaNavigation)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipVia)
                .HasConstraintName("FK_Orders_Shippers");
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.HasIndex(e => e.CategoryId)
                .HasDatabaseName("CategoryID");

            entity.HasIndex(e => e.ProductName)
                .HasDatabaseName("ProductName");

            entity.HasIndex(e => e.SupplierId)
                .HasDatabaseName("SuppliersProducts");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.Property(e => e.Discontinued).HasDefaultValueSql("(0)");

            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

            entity.Property(e => e.ReorderLevel).HasDefaultValueSql("(0)");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasDefaultValueSql("(0)");

            entity.Property(e => e.UnitsInStock).HasDefaultValueSql("(0)");

            entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("(0)");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.RegionId)
                .HasColumnName("RegionID")
                .ValueGeneratedNever();

            entity.Property(e => e.RegionDescription)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
        });

        modelBuilder.Entity<Shippers>(entity =>
        {
            entity.HasKey(e => e.ShipperId);

            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.Phone).HasMaxLength(24);
        });

        modelBuilder.Entity<Suppliers>(entity =>
        {
            entity.HasKey(e => e.SupplierId);

            entity.HasIndex(e => e.CompanyName)
                .HasDatabaseName("CompanyName");

            entity.HasIndex(e => e.PostalCode)
                .HasDatabaseName("PostalCode");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.Property(e => e.Address).HasMaxLength(60);

            entity.Property(e => e.City).HasMaxLength(15);

            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.ContactName).HasMaxLength(30);

            entity.Property(e => e.ContactTitle).HasMaxLength(30);

            entity.Property(e => e.Country).HasMaxLength(15);

            entity.Property(e => e.Fax).HasMaxLength(24);

            entity.Property(e => e.HomePage).HasColumnType("ntext");

            entity.Property(e => e.Phone).HasMaxLength(24);

            entity.Property(e => e.PostalCode).HasMaxLength(15);

            entity.Property(e => e.Region).HasMaxLength(15);
        });

        modelBuilder.Entity<Territories>(entity =>
        {
            entity.HasKey(e => e.TerritoryId);

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasMaxLength(20)
                .ValueGeneratedNever();

            entity.Property(e => e.RegionId).HasColumnName("RegionID");

            entity.Property(e => e.TerritoryDescription)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

            entity.HasOne(d => d.Region)
                .WithMany(p => p.Territories)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Territories_Region");
        });
    }
}
