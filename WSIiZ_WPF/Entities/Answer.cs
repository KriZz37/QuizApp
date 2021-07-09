using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Entities.Interfaces;

namespace QuizApp.Entities
{
    public class Answer : Entity, IHasTitle
    {
        public string Title { get; set; }
        public Question Question { get; set; }
        public long QuestionId { get; set; }
        public bool IsCorrect 
        {
            get => Question.CorrectAnswerId == Id;
        }
    }
}
