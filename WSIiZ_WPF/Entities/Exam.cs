using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Interfaces;

namespace WSIiZ_WPF.Entities
{
    public class Exam : Entity, IHasTitle
    {
        public string Title { get; set; }
        public Folder Folder { get; set; }
        public long FolderId { get; set; }
        public List<Question> Questions { get; set; } = new();
    }
}
