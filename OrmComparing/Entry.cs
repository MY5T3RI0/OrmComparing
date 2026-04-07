using LinqToDB;
using LinqToDB.Extensions.DependencyInjection;
using LinqToDB.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrmComparing.Comparing;
using OrmComparing.DbContexts;
using OrmComparing.Repos;

namespace OrmComparing;

public static class Entry
{
    public static IServiceCollection AddOrms(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");

        serviceCollection.AddDbContext<EfCoreContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        serviceCollection.AddLinqToDBContext<Linq2dbConnection>((provider, options)
            => options
                .UseConnectionString(ProviderName.SqlServer, connectionString!)
                .UseDefaultLogging(provider));

        serviceCollection.AddTransient(_ => new DapperContext(connectionString!));

        serviceCollection.AddTransient<IComparingOrm, EfCoreRepo>();
        serviceCollection.AddTransient<IComparingOrm, Linq2dbRepo>();
        serviceCollection.AddTransient<IComparingOrm, DapperRepo>();

        return serviceCollection;
    }
}
