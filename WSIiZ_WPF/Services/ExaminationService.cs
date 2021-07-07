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

        public void AddQuestion(Exam exam, string title)
        {
            _dataContext.Questions.Add(
                new Question
                {
                    Exam = exam,
                    Title = title
                });

            SaveChanges();
        }

        public List<Question> GetQuestions(Exam exam)
        {
            return _dataContext.Questions.Where(x => x.Exam == exam).ToList();
        }

        public void DeleteQuestion(Question question)
        {
            _dataContext.Questions.Remove(question);
            SaveChanges();
        }

        public void AddAnswer(Question question, string answer)
        {
            _dataContext.Answers.Add(
                new Answer
                {
                    Title = answer,
                    Question = question
                });

            SaveChanges();
        }

        public void DeleteAnswer(Answer answer)
        {
            _dataContext.Answers.Remove(answer);
            SaveChanges();
        }

        public void ToggleCorrectAnswer(Answer answer)
        {
            answer.Question.CorrectAnswerId = answer.Id;
            SaveChanges();
        }
    }
}
