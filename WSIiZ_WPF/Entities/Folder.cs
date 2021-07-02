using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Interfaces;

namespace WSIiZ_WPF.Entities
{
    public class Folder : Entity, IHasTitle, IRemovable
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
        public Folder Parent { get; set; }
        public List<Folder> Subfolders { get; set; } = new();
        public List<Exam> Exams { get; set; } = new();

        // Displays the tree
        public IEnumerable<IHasTitle> SubfoldersWithExams
        {
            get => Subfolders.Concat<IHasTitle>(Exams);
        }
    }
}
