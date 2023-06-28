using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StorageQueuesSample.Writer.Invocables;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScheduler();
        services.AddTransient<RegisterUser>();
    });

var host = builder.Build();
    
host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<RegisterUser>()
        .EveryFiveSeconds();
});

host.Start();

Console.ReadLine();