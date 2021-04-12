using DataTransformer.Core.Config;
using DataTransformer.Core.Data;
using DataTransformer.Core.Pipeline;
using DataTransformer.Core.Plugin;
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
            configBuilder.AddJsonFile("appSettings.json", optional:false, reloadOnChange:true);
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
            services.AddScoped<IConfigDbContext, ConfigDbContext>();
            services.AddScoped<ITypeFinder, TypeFinder>();
            services.AddScoped<IPathUtility, PathUtility>();
            services.AddScoped<IPluginMetadataFactory, PluginMetadataFactory>();
            services.AddScoped<IPluginMetadataRepository, PluginMetadataRepository>();
            services.AddScoped<IPluginLoader, PluginLoader>();
            services.AddScoped<IPluginListValidator, PluginListValidator>();
            services.AddScoped<IPipelineFactory, PipelineFactory>();
            services.AddScoped<IPipelineRepository, PipelineRepository>();
            services.AddScoped<IPipelineExecuter, PipelineExecuter>();
            services.AddScoped<IPipelineValidator, PipelineValidator>();
            services.AddScoped<IPipelineDialogFactory, PipelineDialogFactory>();
            services.AddScoped<MainForm>();
            services.AddTransient<PipelineDialog>();
        }
    }
}
