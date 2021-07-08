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
using WSIiZ_WPF.Entities;

namespace WSIiZ_WPF.Views
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
        public Exam Exam { get; }
        public List<Question> Questions { get; }

        private readonly List<RadioButton> _list = new();

        public ExamWindow(Exam exam)
        {
            Exam = exam;
            Questions = exam.Questions;
            DataContext = this;

            InitializeComponent();
        }
        private void RadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            _list.Add(sender as RadioButton);
        }

        private void Result_Button_Click(object sender, RoutedEventArgs e)
        {
            var checkedRadioButtons = _list.Where(x => x.IsChecked == true);

            List<Answer> checkedAnswers = new();
            foreach (var radioButton in checkedRadioButtons)
            {
                var answer = radioButton.DataContext as Answer;
                checkedAnswers.Add(answer);
            }

            var correctChecked = checkedAnswers.Where(a => a.Question.CorrectAnswerId == a.Id);

            var result = (decimal)correctChecked.Count() / Questions.Count * 100;
            var roundResult = Math.Round(result, 2);
            tbResult.Text = roundResult + "%";
        }
    }
}
