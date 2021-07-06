using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Services;
using WSIiZ_WPF.Utilities;

namespace WSIiZ_WPF.ViewModels
{
    public class ExamDesignViewModel : BaseViewModel, IActivable
    {
        private Exam _exam;
        private readonly ExaminationService _examinationService;

        public ExamDesignViewModel(ExaminationService examinationService)
        {
            _examinationService = examinationService;

            AddQuestionCmd = new RelayCommand(c => AddQuestion());
        }

        public void Activate(object paramater)
        {
            _exam = paramater as Exam;
            Questions = new ObservableCollection<Question>(_exam.Questions);
        }

        // Commands
        public ICommand AddQuestionCmd { get; set; }

        // View properties
        private ObservableCollection<Question> _questions;
        public ObservableCollection<Question> Questions
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
        public void AddQuestion()
        {
            //TODO: QuestionName name validation

            Questions = _examinationService.AddQuestion(_exam, QuestionName);
            QuestionName = string.Empty;
        }
    }
}
