using Serilog.Events;
using Serilog;

namespace ProgettoBackend_S7_L5.Services
{
    public class LoggerService
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Async(a => a.File("Logs/logs_.txt", rollingInterval: RollingInterval.Day))
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
