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
        private readonly QuizService _quizService;

        public QuizDesignViewModel(QuizService quizService)
        {
            _quizService = quizService;

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

            _quizService.AddQuestion(Quiz, QuestionName);
            QuestionName = string.Empty;
            UpdateQuestions();
        }

        public void DeleteQuestion(Question question)
        {
            _quizService.DeleteQuestion(question);
            UpdateQuestions();
        }

        public void AddAnswer(Question question)
        {
            var dialog = new TextDialog();
            if (dialog.ShowDialog() == false) return;

            var answer = dialog.ResponseText;
            if (!NameIsValid(answer)) return;

            _quizService.AddAnswer(question, answer);
            UpdateQuestions();
        }

        public void ToggleCorrectAnswer(Answer answer)
        {
            _quizService.ToggleCorrectAnswer(answer);
            UpdateQuestions();
        }

        public void DeleteAnswer(Answer answer)
        {
            _quizService.DeleteAnswer(answer);
            UpdateQuestions();
        }

        public void ChangeTitle(IHasTitle entity)
        {
            var dialog = new TextDialog();
            if (dialog.ShowDialog() == false) return;

            var newTitle = dialog.ResponseText;
            if (!NameIsValid(newTitle)) return;

            _quizService.ChangeTitle(entity, newTitle);
            UpdateQuestions();
        }

        private void UpdateQuestions()
        {
            Questions = _quizService.GetQuestions(Quiz);
        }
    }
}
