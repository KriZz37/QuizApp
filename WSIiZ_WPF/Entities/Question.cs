using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Entities.Interfaces;

namespace WSIiZ_WPF.Entities
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
