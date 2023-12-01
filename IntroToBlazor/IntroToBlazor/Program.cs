using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Serilog;
using Serilog.Events;
using IntroToBlazor.Client.Pages;
using IntroToBlazor.Components;
using IntroToBlazor.Data;
using IntroToBlazor.Services;

// Initial logger
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    // Replaces bootstrap logger
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Services(services)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();

    builder.Services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IGameService, GameService>();

    var app = builder.Build();

    app.UseSerilogRequestLogging(); // we can add options here

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(Counter).Assembly);

    app.Run();

}
catch(HostAbortedException ex)
{
    Log.Fatal(ex, "Ignore if doing a migration.");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}