using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Entities.Interfaces;

namespace WSIiZ_WPF.Entities
{
    public class Answer : Entity
    {
        public string Title { get; set; }
        public Question Question { get; set; }
        public long QuestionId { get; set; }
    }
}
