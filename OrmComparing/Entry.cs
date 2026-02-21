using LinqToDB;
using LinqToDB.Extensions.DependencyInjection;
using LinqToDB.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrmComparing.DbContexts;

namespace OrmComparing;

public static class Entry
{
    private static readonly Action<DbContextOptionsBuilder> DefaultOptionsAction = _ => { };

    /// <summary>
    ///     Добавления зависимостей для работы с БД
    /// </summary>
    public static IServiceCollection AddOrms(this IServiceCollection serviceCollection, IConfiguration configuration,
        Action<DbContextOptionsBuilder> optionsAction)
    {
        serviceCollection.AddDbContext<EfCoreContext>(optionsAction ?? DefaultOptionsAction);

        serviceCollection.AddLinqToDBContext<Linq2dbConnection>((provider, options)
            => options
                .UseConnectionString(ProviderName.PostgreSQL, configuration.GetConnectionString("PostgreSql")!)
                .UseDefaultLogging(provider));
    
        return serviceCollection;
    }
}
