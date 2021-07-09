using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Data;
using QuizApp.Entities;

namespace QuizApp.Services
{
    public class QuizService : BaseService
    {
        public QuizService(DataContext dataContext) : base(dataContext) { }

        public void AddQuestion(Quiz quiz, string title)
        {
            _dataContext.Questions.Add(
                new Question
                {
                    Quiz = quiz,
                    Title = title
                });

            SaveChanges();
        }

        public List<Question> GetQuestions(Quiz quiz)
        {
            return _dataContext.Questions.Where(x => x.Quiz == quiz).ToList();
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

        public decimal CalculateResult(int correctCheckedCount, int questionCount)
        {
            if (questionCount == 0) return -1;

            var result = (decimal)correctCheckedCount / questionCount * 100;
            var roundResult = Math.Round(result, 2);

            return roundResult;
        }
    }
}
