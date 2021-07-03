using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Entities.Interfaces;

namespace WSIiZ_WPF.Entities
{
    public class Question : Entity
    {
        public string Title { get; set; }
        public Exam Exam { get; set; }
        public long ExamId { get; set; }
        public long CorrectAnswerId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
