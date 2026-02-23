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
        serviceCollection.AddDbContext<EfCoreContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")!);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        serviceCollection.AddLinqToDBContext<Linq2dbConnection>((provider, options)
            => options
                .UseConnectionString(ProviderName.SqlServer, configuration.GetConnectionString("SqlServer")!)
                .UseDefaultLogging(provider));

        serviceCollection.AddTransient<IComparingOrm, EfCoreRepo>();
        serviceCollection.AddTransient<IComparingOrm, Linq2dbRepo>();

        return serviceCollection;
    }
}
