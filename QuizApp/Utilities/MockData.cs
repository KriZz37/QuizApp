using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Entities;

namespace QuizApp.Utilities
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
                            new Answer 
                            { 
                                Title = "CORRECT answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4 answer 4",
                                Id = 1,
                                Question = new Question { CorrectAnswerId = 1} 
                            }
                        }
                    },
                    new Question
                    {
                        Title = "Mock question 2 Mock question 2 Mock question 2 Mock question 2 Mock question 2 Mock question 2?",
                        Answers = new List<Answer>
                        {
                            new Answer { Title = "answer 1" },
                            new Answer 
                            { 
                                Title = "CORRECT answer 4",
                                Id = 1,
                                Question = new Question { CorrectAnswerId = 1}
                            }
                        }
                    },
                    new Question
                    {
                        Title = "Mock question 3?",
                        Answers = new List<Answer>
                        {
                            new Answer 
                            {
                                Title = "CORRECT answer 1",
                                Id = 1,
                                Question = new Question { CorrectAnswerId = 1}
                            },
                            new Answer { Title = "answer 2 answer 2 answer 2 answer 2 answer 2 answer 2" },
                            new Answer { Title = "answer 3 answer 3" },
                        }
                    },
                };
            }
        }

        public List<Folder> TreeFolders
        {
            get
            {
                return new List<Folder>
                {
                    new Folder
                    {
                        Title = "1",
                        Subfolders = new List<Folder>
                        {
                            new Folder
                            {
                                Title = "1.1",
                                Subfolders = new List<Folder>
                                {
                                    new Folder
                                    {
                                        Title = "1.1.1",
                                        Quizzes = new List<Quiz> { new Quiz { Title = "QUIZ 1.1.1" } }
                                    }
                                }
                            },
                            new Folder
                            {
                                Title = "1.2",
                                Subfolders = new List<Folder>
                                {
                                    new Folder
                                    {
                                        Title = "1.2.1",
                                        Quizzes = new List<Quiz> { new Quiz { Title = "QUIZ 1.2.1" } }
                                    },
                                    new Folder
                                    {
                                        Title = "1.3.1",
                                        Quizzes = new List<Quiz> { new Quiz { Title = "QUIZ 1.3.1" } },
                                        Subfolders = new List<Folder>
                                        {
                                            new Folder
                                            {
                                                Title = "1.3.1.1",
                                                Quizzes = new List<Quiz> { new Quiz { Title = "QUIZ 1.3.1.1" } }
                                            },
                                            new Folder
                                            {
                                                Title = "1.3.1.2",
                                                Quizzes = new List<Quiz> { new Quiz { Title = "QUIZ 1.3.1.2" } }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new Folder
                    {
                        Title = "2",
                        Subfolders = new List<Folder>
                        {
                            new Folder
                            {
                                Title = "2.1",
                                Subfolders = new List<Folder> { new Folder { Title = "1.1.1" } }
                            }
                        }
                    },
                    new Folder
                    {
                        Title = "3",
                        Subfolders = new List<Folder>
                        {
                            new Folder
                            {
                                Title = "3.1",
                                Subfolders = new List<Folder>
                                {
                                    new Folder { Title = "3.1.1" },
                                    new Folder { Title = "3.1.2" },
                                    new Folder
                                    {
                                        Title = "3.1.3",
                                        Quizzes = new List<Quiz> { new Quiz { Title = "QUIZ 3.1.3" } }
                                    },
                                    new Folder { Title = "3.1.4" }
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}
