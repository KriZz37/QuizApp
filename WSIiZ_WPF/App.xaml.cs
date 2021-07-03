using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using WSIiZ_WPF.Data;
using WSIiZ_WPF.Services;
using WSIiZ_WPF.Views;

namespace WSIiZ_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var navigationService = ServiceProvider.GetRequiredService<WindowNavigationService>();
            navigationService.ShowWindow<MainWindow>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SQLite")));

            services.AddScoped<WindowNavigationService>();

            services.AddTransient<MainWindow>();
            services.AddTransient<ExamDesignWindow>();
                                 
            services.AddTransient<TreeService>();
            services.AddTransient<DataService>();
            services.AddTransient<ExaminationService>();
        }
    }
}
