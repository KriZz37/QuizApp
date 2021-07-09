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
using QuizApp.Entities;
using QuizApp.Entities.Interfaces;
using QuizApp.Services;
using QuizApp.Utilities;
using QuizApp.ViewModels;

namespace QuizApp.Views
{
    /// <summary>
    /// Interaction logic for QuizDesignWindow.xaml
    /// </summary>
    public partial class QuizDesignWindow : Window, IActivable
    {
        private QuizDesignViewModel _quizDesignViewModel;
        private readonly ServiceGenerator _serviceGenerator;

        public QuizDesignWindow(ServiceGenerator serviceGenerator)
        {
            _serviceGenerator = serviceGenerator;

            InitializeComponent();
        }

        public void Activate(object paramater)
        {
            var quiz = paramater as Quiz;
            _quizDesignViewModel = _serviceGenerator.CreateViewModel<QuizDesignViewModel>(quiz);

            DataContext = _quizDesignViewModel;
        }

        //TODO: Move methods below to ViewModel
        private void DeleteQuestion_Button_Click(object sender, RoutedEventArgs e)
        {
            var question = (e.OriginalSource as Button).DataContext as Question;
            _quizDesignViewModel.DeleteQuestion(question);
        }

        private void AddAnswer_Button_Click(object sender, RoutedEventArgs e)
        {
            var question = (e.OriginalSource as Button).DataContext as Question;
            _quizDesignViewModel.AddAnswer(question);
        }

        private void ToggleCorrectAnswer_Button_Click(object sender, RoutedEventArgs e)
        {
            var answer = (e.OriginalSource as Button).DataContext as Answer;
            _quizDesignViewModel.ToggleCorrectAnswer(answer);
        }

        private void Delete_Answer_Button_Click(object sender, RoutedEventArgs e)
        {
            var answer = (e.OriginalSource as Button).DataContext as Answer;
            _quizDesignViewModel.DeleteAnswer(answer);
        }

        private void ChangeTitle_Button_Click(object sender, RoutedEventArgs e)
        {
            var entity = (e.OriginalSource as Button).DataContext as IHasTitle;
            _quizDesignViewModel.ChangeTitle(entity);
        }
    }
}
