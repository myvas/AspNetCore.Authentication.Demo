using Demo;
using Demo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

var assembly = typeof(Program).Assembly;
var assemblyName = assembly.GetName().Name;
var assemblyVersion = assembly.GetName().Version?.ToString()
    ?? assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
Console.WriteLine($"{assemblyName} v{assemblyVersion} starting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Set default logging level.
#if DEBUG
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
#else
    builder.Logging.SetMinimumLevel(LogLevel.Information);
#endif

    var app = builder.ConfigureServices().Build()
        .MigrateDatabase()
        .SeedDatabase("demo", "demo@myvas.com")
        .ConfigurePipeline();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"{assemblyName} v{assemblyVersion} terminated for an unhandled exception occured.");
    Console.WriteLine(ex);
}
finally
{
    Console.WriteLine($"{assemblyName} v{assemblyVersion} shutdown.");
}
