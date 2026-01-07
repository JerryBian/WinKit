using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WinKit.HostedServices;
using WinKit.Logging;
using WinKit.Options;
using WinKit.Store;

namespace WinKit
{
    internal static class Program
    {
        private static Mutex _mutex;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NativeMethods.SetAppUserModelId("WinKit");

            ApplicationConfiguration.Initialize();

            using var host =
                Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Debug);
                        builder.ClearProviders();
                        builder.AddDebug();
                    });

                    services.Configure<WinKitOption>(context.Configuration.GetSection("app"));
                    services.PostConfigure<WinKitOption>(options =>
                    {
                        if (string.IsNullOrEmpty(options.AppDataFolder))
                        {
                            options.AppDataFolder = Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                "WinKit");
                            Directory.CreateDirectory(options.AppDataFolder);
                        }
                    });

                    services.AddSingleton<IFileLogger, FileLogger>();
                    services.AddSingleton<IUserSettingStore, UserSettingStore>();
                    services.AddSingleton<MainForm>();

                    services.AddHostedService<AutoMouseMoverHostedService>();
                    services.AddHostedService<AutoShutdownPCHostedService>();
                })
                .Build();
            
            var logger = host.Services.GetRequiredService<IFileLogger>();
            logger.Initialize("startup");

#if DEBUG
            var mutexName = "WinKit_Debug_SingleInstance";
            var uniqueIdentifier = "Debug";
#else
            var mutexName = "WinKit_Release_SingleInstance";
            var uniqueIdentifier = "Release";
#endif

            _mutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                logger.LogAsync("Another instance is already running. Exiting.").Wait();
                NativeMethods.BroadcastShowMeMessage(uniqueIdentifier);
                return;
            }

            var mainForm = host.Services.GetRequiredService<MainForm>();
            mainForm.SetShowMeMessage(NativeMethods.GetShowMeMessage(uniqueIdentifier));

            host.Start();

            try
            {
                Application.Run(mainForm);
            }
            finally
            {
                _mutex?.ReleaseMutex();
                _mutex?.Dispose();
            }
        }
    }
}