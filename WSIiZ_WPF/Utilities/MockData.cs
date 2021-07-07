using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Entities;

namespace WSIiZ_WPF.Utilities
{
    public class MockData
    {
        public List<Question> Questions
        {
            get
            {
                return new List<Question>
                {
                    new Question
                    {
                        Title = "Mock question 1 Mock question 1 Mock question 1?",
                        Answers = new List<Answer>
                        {
                            new Answer { Title = "answer 1" },
                            new Answer { Title = "answer 2 answer 2 answer 2 answer 2 answer 2 answer 2" },
                            new Answer { Title = "answer 3 answer 3 answer 3" },
                            new Answer { Title = "answer 4 answer 4 answer 4 answer 4" }
                        }
                    },
                    new Question
                    {
                        Title = "Mock question 2 Mock question 2 Mock question 2 Mock question 2 Mock question 2 Mock question 2?",
                        Answers = new List<Answer>
                        {
                            new Answer { Title = "answer 1" },
                            new Answer { Title = "answer 4" }
                        }
                    },
                    new Question
                    {
                        Title = "Mock question 3?",
                        Answers = new List<Answer>
                        {
                            new Answer { Title = "answer 1 answer 1" },
                            new Answer { Title = "answer 2 answer 2 answer 2 answer 2 answer 2 answer 2" },
                            new Answer { Title = "answer 3 answer 3" },
                        }
                    },
                };
            }
        }
    }
}
