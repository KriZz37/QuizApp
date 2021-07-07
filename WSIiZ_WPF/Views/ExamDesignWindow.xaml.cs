using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Entities.Interfaces;
using WSIiZ_WPF.Services;
using WSIiZ_WPF.Utilities;
using WSIiZ_WPF.ViewModels;

namespace WSIiZ_WPF.Views
{
    /// <summary>
    /// Interaction logic for ExamDesignWindow.xaml
    /// </summary>
    public partial class ExamDesignWindow : Window, IActivable
    {
        private ExamDesignViewModel _examDesignViewModel;
        private readonly ServiceGenerator _serviceGenerator;

        public ExamDesignWindow(ServiceGenerator serviceGenerator)
        {
            _serviceGenerator = serviceGenerator;

            InitializeComponent();
        }

        public void Activate(object paramater)
        {
            var exam = paramater as Exam;
            _examDesignViewModel = _serviceGenerator.CreateViewModel<ExamDesignViewModel>(exam);

            DataContext = _examDesignViewModel;
        }

        //TODO: Move methods below to ViewModel
        private void DeleteQuestion_Button_Click(object sender, RoutedEventArgs e)
        {
            var question = (e.OriginalSource as Button).DataContext as Question;
            _examDesignViewModel.DeleteQuestion(question);
        }

        private void AddAnswer_Button_Click(object sender, RoutedEventArgs e)
        {
            var question = (e.OriginalSource as Button).DataContext as Question;
            _examDesignViewModel.AddAnswer(question);
        }

        private void ToggleCorrectAnswer_Button_Click(object sender, RoutedEventArgs e)
        {
            var answer = (e.OriginalSource as Button).DataContext as Answer;
            _examDesignViewModel.ToggleCorrectAnswer(answer);
        }

        private void Delete_Answer_Button_Click(object sender, RoutedEventArgs e)
        {
            var answer = (e.OriginalSource as Button).DataContext as Answer;
            _examDesignViewModel.DeleteAnswer(answer);
        }

        private void ChangeTitle_Button_Click(object sender, RoutedEventArgs e)
        {
            var entity = (e.OriginalSource as Button).DataContext as IHasTitle;
            _examDesignViewModel.ChangeTitle(entity);
        }
    }
}
