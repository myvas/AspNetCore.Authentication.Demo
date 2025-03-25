using Demo;
using Demo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var assembly = typeof(Program).Assembly;
var assemblyName = assembly.GetName().Name;
var assemblyVersion = assembly.GetName().Version?.ToString()
    ?? assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
Log.Information($"{assemblyName} v{assemblyVersion} starting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.ConfigureServices().Build()
        .MigrateDatabase()
        .SeedDatabase("demo", "demo@myvas.com")
        .ConfigurePipeline();

    app.Run();
}
catch (Exception ex)
{
    // Any unhandled exception during start-up will be caught and flushed to
    // our log file or centralized log server
    Log.Fatal(ex, "Host terminated for an unhandled exception occured.");
}
finally
{
    Log.Information($"{assemblyName} v{assemblyVersion} shutdown.");
    Log.CloseAndFlush();
}
