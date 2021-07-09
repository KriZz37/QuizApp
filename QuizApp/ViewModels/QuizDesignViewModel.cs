using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuizApp.Entities;
using QuizApp.Entities.Interfaces;
using QuizApp.Services;
using QuizApp.Utilities;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// QuizDesignWindow ViewModel
    /// </summary>
    public class QuizDesignViewModel : BaseViewModel, IActivable
    {
        public Quiz Quiz { get; set; }
        private readonly QuizService _qizzService;

        public QuizDesignViewModel(QuizService quizService)
        {
            _qizzService = quizService;

            AddQuestionCmd = new RelayCommand(c => AddQuestion());
        }

        public void Activate(object paramater)
        {
            Quiz = paramater as Quiz;
            Questions = new List<Question>(Quiz.Questions);
        }

        // Commands
        public ICommand AddQuestionCmd { get; set; }

        // View properties
        private List<Question> _questions;
        public List<Question> Questions
        {
            get => _questions;
            set => OnPropertyChanged(ref _questions, value);
        }

        private string _questionName;
        public string QuestionName
        {
            get => _questionName;
            set => OnPropertyChanged(ref _questionName, value);
        }

        // Command methods
        private void AddQuestion()
        {
            if (!NameIsValid(QuestionName)) return;

            _qizzService.AddQuestion(Quiz, QuestionName);
            QuestionName = string.Empty;
            UpdateQuestions();
        }

        public void DeleteQuestion(Question question)
        {
            _qizzService.DeleteQuestion(question);
            UpdateQuestions();
        }

        public void AddAnswer(Question question)
        {
            var dialog = new TextDialog();
            if (dialog.ShowDialog() == false) return;

            var answer = dialog.ResponseText;
            if (!NameIsValid(answer)) return;

            _qizzService.AddAnswer(question, answer);
            UpdateQuestions();
        }

        public void ToggleCorrectAnswer(Answer answer)
        {
            _qizzService.ToggleCorrectAnswer(answer);
            UpdateQuestions();
        }

        public void DeleteAnswer(Answer answer)
        {
            _qizzService.DeleteAnswer(answer);
            UpdateQuestions();
        }

        public void ChangeTitle(IHasTitle entity)
        {
            var dialog = new TextDialog();
            if (dialog.ShowDialog() == false) return;

            var newTitle = dialog.ResponseText;
            if (!NameIsValid(newTitle)) return;

            _qizzService.ChangeTitle(entity, newTitle);
            UpdateQuestions();
        }

        private void UpdateQuestions()
        {
            Questions = _qizzService.GetQuestions(Quiz);
        }
    }
}
