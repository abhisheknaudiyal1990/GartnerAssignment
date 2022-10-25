using Gartner.DataContracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Gartner
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton<IDataService, FileService>()
                //.AddSingleton<IDataService, DBService>() // We can extend fucntionality with change database from MySQL to MongoDB later
                .BuildServiceProvider();

            //setup our Logging
            var logger = serviceProvider.GetService<ILoggerFactory>()
             .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            var bar = serviceProvider.GetService<IDataService>();
            bar.ParseData();
        }
    }
}
