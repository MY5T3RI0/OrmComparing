using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrmComparing;
using OrmComparing.Comparing;
using OrmComparing.DbContexts;
using System.Diagnostics;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var services = builder.Services;
var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

services.AddOrms(configuration);

using IHost host = builder.Build();

var ormComparer = new OrmComparer(host.Services.GetServices<IComparingOrm>().ToList());

var compareResults = ormComparer.CompareAllOperations();

foreach (var compareResult in compareResults)
    Console.WriteLine(compareResult);

var stopwatch = new Stopwatch();

var db = host.Services.GetRequiredService<Linq2dbConnection>();

var list =
            (
                from o in db.Orders
                join c in db.Customers on o.CustomerId equals c.CustomerId
                select new { o.OrderId, o.OrderDate, c.Country, c.CompanyName }
            ).Take(10).ToList().Count();

stopwatch.Start();

list =
            (
                from o in db.Orders
                join c in db.Customers on o.CustomerId equals c.CustomerId
                select new { o.OrderId, o.OrderDate, c.Country, c.CompanyName }
            ).Take(10).ToList().Count();

stopwatch.Stop();

Console.WriteLine($"With manual query exection elapsed {stopwatch.Elapsed}ms");

await host.RunAsync();
