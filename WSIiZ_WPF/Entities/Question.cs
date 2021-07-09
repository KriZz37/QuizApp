using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Entities.Interfaces;

namespace QuizApp.Entities
{
    public class Question : Entity, IHasTitle
    {
        public string Title { get; set; }
        public Quiz Quiz { get; set; }
        public long QuizId { get; set; }
        public long CorrectAnswerId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
