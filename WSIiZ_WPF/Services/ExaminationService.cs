using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Data;
using WSIiZ_WPF.Entities;

namespace WSIiZ_WPF.Services
{
    public class ExaminationService : BaseService
    {
        public ExaminationService(DataContext dataContext) : base(dataContext) { }

        public ObservableCollection<Question> AddQuestion(Exam exam, string title)
        {
            _dataContext.Questions.Add(
                new Question
                {
                    Exam = exam,
                    Title = title
                });

            SaveChanges();

            return GetQuestions(exam);
        }

        private ObservableCollection<Question> GetQuestions(Exam exam)
        {
            var questions = _dataContext.Questions.Where(x => x.Exam == exam);
            return new ObservableCollection<Question>(questions);
        }
    }
}
