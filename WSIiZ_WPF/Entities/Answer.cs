using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Interfaces;

namespace WSIiZ_WPF.Entities
{
    public class Answer : Entity, IHasTitle
    {
        public string Title { get; set; }
        public Question Question { get; set; }
        public long QuestionId { get; set; }
    }
}
