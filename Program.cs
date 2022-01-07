global using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using GHActions_EFMigrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new ConfigurationBuilder();
BuildConfig(builder);

var host = CreateHostBuilder(args).Build();


static void BuildConfig(IConfigurationBuilder builder)
{
    var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env}.json",
            optional: true)
        .AddEnvironmentVariables()
        .Build();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(connectionString));
        });
