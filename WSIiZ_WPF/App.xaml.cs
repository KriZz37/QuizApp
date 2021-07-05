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
using WSIiZ_WPF.ViewModels;
using WSIiZ_WPF.Views;

namespace WSIiZ_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration;
        private DataService _dataService;

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _dataService = _serviceProvider.GetRequiredService<DataService>();
            _dataService.EnsureDbCreated();

            var serviceGenerator = _serviceProvider.GetRequiredService<ServiceGenerator>();
            serviceGenerator.ShowWindow<TreeWindow>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("SQLite")));

            services.AddScoped<ServiceGenerator>();

            services.AddTransient<TreeWindow>();
            services.AddTransient<ExamDesignWindow>();

            services.AddTransient<ExamDesignViewModel>();
                                 
            services.AddTransient<TreeService>();
            services.AddTransient<DataService>();
            services.AddTransient<ExaminationService>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _dataService.Dispose();
        }
    }
}
