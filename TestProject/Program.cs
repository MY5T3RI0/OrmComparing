using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrmComparing;
using OrmComparing.Comparing;

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

await host.RunAsync();
