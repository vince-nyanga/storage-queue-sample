using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StorageQueuesSample.Reader.Invocables;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScheduler();
        services.AddTransient<SendWelcomeEmail>();
    });

var host = builder.Build();
    
host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<SendWelcomeEmail>()
        .EverySecond();
});

host.Start();

Console.ReadLine();