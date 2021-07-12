using System;
using System.Collections.Generic;
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
using QuizApp.Services;
using QuizApp.Utilities;

namespace QuizApp.Views
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window, IActivable
    {
        public Quiz Quiz { get; private set; }
        public List<Question> Questions { get; private set; }

        private readonly List<RadioButton> _radioButtonList = new();
        private readonly QuizService _quizService;

        public QuizWindow(QuizService quizService)
        {
            _quizService = quizService;

            InitializeComponent();
        }

        public void Activate(object paramater)
        {
            Quiz = paramater as Quiz;
            Questions = Quiz.Questions;
            DataContext = this;
        }

        /// <summary>
        /// Register all radio buttons.
        /// </summary>
        private void RadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            _radioButtonList.Add(sender as RadioButton);
        }

        private void Result_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Questions.Count == 0)
            {
                MessageBox.Show("Quiz nie zawiera pytań!");
                return;
            }

            var checkedRadioButtons = _radioButtonList.Where(x => x.IsChecked == true);

            List<Answer> checkedAnswers = new();
            foreach (var radioButton in checkedRadioButtons)
            {
                var answer = radioButton.DataContext as Answer;
                checkedAnswers.Add(answer);
            }

            var correctCheckedCount = checkedAnswers.Where(a => a.Question.CorrectAnswerId == a.Id).Count();

            var result = _quizService.CalculateResult(correctCheckedCount, Questions.Count);
            tbResult.Text = result + "%";

            resultBox.Visibility = Visibility.Visible;
        }
    }
}
