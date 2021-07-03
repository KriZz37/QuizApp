using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Entities.Interfaces;

namespace WSIiZ_WPF.Entities
{
    public class Folder : Entity, ITreeItem
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public Folder Parent { get; set; }
        public List<Folder> Subfolders { get; set; } = new();
        public List<Exam> Exams { get; set; } = new();

        // Displays the tree
        public IEnumerable<ITreeItem> SubfoldersWithExams
        {
            get => Subfolders.Concat<ITreeItem>(Exams);
        }
    }
}
