using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using QuizApp.ViewModels;

namespace QuizApp.Utilities
{
    public interface IActivable
    {
        void Activate(object paramater);
    }

    public class ServiceGenerator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceGenerator(IServiceProvider serviceProvider)
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

        public TViewModel CreateViewModel<TViewModel>(object paramter = null) where TViewModel : BaseViewModel
        {
            var vm = _serviceProvider.GetRequiredService<TViewModel>();

            if (vm is IActivable activableVM)
            {
                activableVM.Activate(paramter);
            }

            return vm;
        }
    }
}
