using DataTransformer.Core.Config;
using DataTransformer.Core.Factories;
using DataTransformer.Core.Services;
using DataTransformer.Core.Utility;
using DataTransformer.DesktopApp.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace DataTransformer.DesktopApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appSettings.json");
            var config = configBuilder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services, config);

            using (var sp = services.BuildServiceProvider())
            {
                var mainForm = sp.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }

        }

        private static void ConfigureServices(ServiceCollection services, IConfiguration config)
        {
            services.AddOptions<LibraryConfiguration>().Configure(libraryConfig =>
            {
                config.Bind(libraryConfig);
            });
            services.AddSingleton<ITypeFinder, TypeFinder>();
            services.AddSingleton<IPluginLoader, PluginLoader>();
            services.AddSingleton<IPipelineFactory, PipelineFactory>();
            services.AddSingleton<IPipelineService, PipelineService>();
            services.AddSingleton<IPipelineExecuter, PipelineExecuter>();
            services.AddSingleton<IPipelineDialogFactory, PipelineDialogFactory>();
            services.AddScoped<MainForm>();
            services.AddTransient<PipelineDialog>();
        }
    }
}
