using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// Base for all ViewModels, imprements INotifyPropertyChanged,
    /// also includes common methods used by ViewModels.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected bool NameIsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Nazwa nie może być pusta");
                return false;
            }

            return true;
        }
    }
}
