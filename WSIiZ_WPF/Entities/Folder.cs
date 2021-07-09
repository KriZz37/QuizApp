using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Entities.Interfaces;

namespace QuizApp.Entities
{
    public class Folder : Entity, ITreeItem
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public Folder Parent { get; set; }
        public List<Folder> Subfolders { get; set; } = new();
        public List<Quiz> Quizzes { get; set; } = new();

        // Displays the tree
        public IEnumerable<ITreeItem> SubfoldersWithQuizzes
        {
            get => Subfolders.Concat<ITreeItem>(Quizzes);
        }

        // Folder image
        public bool IsExpanded { get; set; }
    }
}
