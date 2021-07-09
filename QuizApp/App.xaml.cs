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
using QuizApp.Data;
using QuizApp.Services;
using QuizApp.ViewModels;
using QuizApp.Views;
using QuizApp.Utilities;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration;
        private DataService _dataService;

        /// <summary>
        /// Method called when the application is starting
        /// </summary>
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

        /// <summary>
        /// Register services
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("SQLite")));

            services.AddScoped<ServiceGenerator>();

            services.AddTransient<TreeWindow>();
            services.AddTransient<QuizDesignWindow>();
            services.AddTransient<QuizWindow>();

            services.AddTransient<TreeViewModel>();
            services.AddTransient<QuizDesignViewModel>();
                                 
            services.AddTransient<TreeService>();
            services.AddTransient<DataService>();
            services.AddTransient<QuizService>();
        }

        /// <summary>
        /// Method called when exiting the application.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _dataService.Dispose();
        }
    }
}
