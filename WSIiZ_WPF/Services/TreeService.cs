using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Data;
using WSIiZ_WPF.Entities;

namespace WSIiZ_WPF.Services
{
    public class TreeService : BaseService
    {
        public TreeService(DataContext dataContext) : base(dataContext) { }

        public List<Folder> GetRootFolders()
        {
            // Get whole tree
            return _dataContext.Folders
                .AsEnumerable()
                .Where(x => x.Parent == null)
                .ToList();
        }
    }
}
