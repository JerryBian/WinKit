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
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NativeMethods.SetAppUserModelId("WinKit");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
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
            host.Start();

            Application.Run(host.Services.GetRequiredService<MainForm>());
        }
    }
}