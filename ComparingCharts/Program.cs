using ApexCharts;
using ComparingCharts.Components;
using OrmComparing;
using OrmComparing.Comparing;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddApexCharts();

builder.Services.AddOrms(configuration);

builder.Services.AddTransient(x => x.GetServices<IComparingOrm>().ToList());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
