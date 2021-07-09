using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Entities.Interfaces;

namespace QuizApp.Entities
{
    public class Quiz : Entity, ITreeItem
    {
        public string Title { get; set; }
        public Folder Folder { get; set; }
        public long FolderId { get; set; }
        public List<Question> Questions { get; set; } = new();
    }
}
