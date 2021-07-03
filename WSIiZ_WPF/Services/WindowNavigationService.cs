using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WSIiZ_WPF.Services
{
    public interface IActivable
    {
        void Activate(object paramater);
    }

    public class WindowNavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowNavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow<TWindow>(object paramter = null) where TWindow : Window
        {
            var window = _serviceProvider.GetRequiredService<TWindow>();

            if (window is IActivable activableWindow)
            {
                activableWindow.Activate(paramter);
            }

            window.Show();
        }
    }
}
