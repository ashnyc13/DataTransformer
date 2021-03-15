using DataTransformer.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace DataTransformer
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

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (var sp = services.BuildServiceProvider())
            {
                var mainForm = sp.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
            
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IPluginService, PluginService>();
            services.AddSingleton<IPipelineService, PipelineService>();
            services.AddScoped<MainForm>();
        }
    }
}
