using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Entities.Interfaces;
using WSIiZ_WPF.Services;
using WSIiZ_WPF.Utilities;

namespace WSIiZ_WPF.ViewModels
{
    public class ExamDesignViewModel : BaseViewModel, IActivable
    {
        public Exam Exam { get; set; }
        private readonly ExaminationService _examinationService;

        public ExamDesignViewModel(ExaminationService examinationService)
        {
            _examinationService = examinationService;

            AddQuestionCmd = new RelayCommand(c => AddQuestion());
        }

        public void Activate(object paramater)
        {
            Exam = paramater as Exam;
            Questions = new List<Question>(Exam.Questions);
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

            _examinationService.AddQuestion(Exam, QuestionName);
            QuestionName = string.Empty;
            UpdateQuestions();
        }

        public void DeleteQuestion(Question question)
        {
            _examinationService.DeleteQuestion(question);
            UpdateQuestions();
        }

        public void AddAnswer(Question question)
        {
            var dialog = new TextDialog();
            if (dialog.ShowDialog() == false) return;

            var answer = dialog.ResponseText;
            if (!NameIsValid(answer)) return;

            _examinationService.AddAnswer(question, answer);
            UpdateQuestions();
        }

        public void ToggleCorrectAnswer(Answer answer)
        {
            _examinationService.ToggleCorrectAnswer(answer);
            UpdateQuestions();
        }

        public void DeleteAnswer(Answer answer)
        {
            _examinationService.DeleteAnswer(answer);
            UpdateQuestions();
        }

        public void ChangeTitle(IHasTitle entity)
        {
            var dialog = new TextDialog();
            if (dialog.ShowDialog() == false) return;

            var newTitle = dialog.ResponseText;
            if (!NameIsValid(newTitle)) return;

            _examinationService.ChangeTitle(entity, newTitle);
            UpdateQuestions();
        }

        private void UpdateQuestions()
        {
            Questions = _examinationService.GetQuestions(Exam);
        }
    }
}
