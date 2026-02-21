using LinqToDB;
using LinqToDB.Data;
using OrmComparing.Entities;

namespace OrmComparing.DbContexts;

public class Linq2dbConnection : DataConnection
{
    public Linq2dbConnection(DataOptions dataOptions) : base(dataOptions) { }

    public ITable<Categories> Categories { get { return this.GetTable<Categories>(); } }
    public ITable<CustomerCustomerDemo> CustomerCustomerDemoes { get { return this.GetTable<CustomerCustomerDemo>(); } }
    public ITable<CustomerDemographics> CustomerDemographics { get { return this.GetTable<CustomerDemographics>(); } }
    public ITable<Customers> Customers { get { return this.GetTable<Customers>(); } }
    public ITable<Employees> Employees { get { return this.GetTable<Employees>(); } }
    public ITable<EmployeeTerritories> EmployeeTerritories { get { return this.GetTable<EmployeeTerritories>(); } }
    public ITable<OrderDetails> OrderDetails { get { return this.GetTable<OrderDetails>(); } }
    public ITable<Orders> Orders { get { return this.GetTable<Orders>(); } }
    public ITable<Products> Products { get { return this.GetTable<Products>(); } }
    public ITable<Region> Regions { get { return this.GetTable<Region>(); } }
    public ITable<Shippers> Shippers { get { return this.GetTable<Shippers>(); } }
    public ITable<Suppliers> Suppliers { get { return this.GetTable<Suppliers>(); } }
    public ITable<Territories> Territories { get { return this.GetTable<Territories>(); } }
}
