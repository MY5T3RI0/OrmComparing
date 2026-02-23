using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrmComparing.DbContexts;
using OrmComparing.Entities;
using System.Collections;

namespace OrmComparing;

public static class DbSeed
{
    public async static Task Fill(EfCoreContext context)
    {
        await AddEntities<OrderDetails>(context);
        await AddEntities<EmployeeTerritories>(context);
        await AddEntities<CustomerCustomerDemo>(context);
    }

    private async static Task AddEntities<T>(EfCoreContext context)
    where T : class
    {
        var fixture = new Fixture();

        fixture.Customize<string>(composer =>
            composer.FromFactory(() =>
            {
                var random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                return new string(
                    Enumerable.Range(0, 15)
                        .Select(_ => chars[random.Next(chars.Length)])
                        .ToArray());
            }));

        fixture.Customize<Employees>(c =>
            c.Without(e => e.ReportsToNavigation)
            .Without(e => e.ReportsTo)
            .Without(e => e.InverseReportsToNavigation));

        fixture.Customize<Territories>(c =>
            c.Without(t => t.RegionId)
            .Without(t => t.Region));

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));

        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var entities = fixture.Create<List<T>>();

        foreach (var entity in entities)
        {
            ResetPrimaryKeysRecursively(
                context,
                entity!,
                new HashSet<object>());
        }

        try
        {
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
        catch { }
    }

    private static void ResetPrimaryKeysRecursively(
        DbContext context,
        object entity,
        HashSet<object> visited)
    {
        if (entity == null || visited.Contains(entity))
            return;

        visited.Add(entity);

        var entityType = context.Model.FindEntityType(entity.GetType());
        if (entityType == null)
            return;

        var primaryKey = entityType.FindPrimaryKey();
        if (primaryKey != null)
        {
            foreach (var property in primaryKey.Properties)
            {
                if (property.ValueGenerated == ValueGenerated.OnAdd)
                {
                    var propInfo = entity.GetType().GetProperty(property.Name);

                    if (propInfo != null && propInfo.CanWrite)
                    {
                        var defaultValue = propInfo.PropertyType.IsValueType
                            ? Activator.CreateInstance(propInfo.PropertyType)
                            : null;

                        propInfo.SetValue(entity, defaultValue);
                    }
                }
            }
        }

        foreach (var navigation in entityType.GetNavigations())
        {
            var propInfo = entity.GetType().GetProperty(navigation.Name);
            var value = propInfo?.GetValue(entity);

            if (value == null)
                continue;

            if (navigation.IsCollection)
            {
                foreach (var item in (IEnumerable)value)
                    ResetPrimaryKeysRecursively(context, item, visited);
            }
            else
            {
                ResetPrimaryKeysRecursively(context, value, visited);
            }
        }
    }
}
