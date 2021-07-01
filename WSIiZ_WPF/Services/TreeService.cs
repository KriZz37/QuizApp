using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Data;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Interfaces;

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

        public void ChangeTitle(IHasTitle entity, string newTitle)
        {
            entity.Title = newTitle;
            SaveChanges();
        }

        public void AddTreeItem(Folder selectedItem, string itemName)
        {
            if (selectedItem is null)
            {
                _dataContext.Folders.Add(
                    new Folder
                    {
                        Parent = null,
                        Title = itemName
                    });
            }
            else
            {
                selectedItem.Subfolders.Add(
                    new Folder
                    {
                        Parent = selectedItem,
                        Title = itemName
                    });
            }

            SaveChanges();
        }

        // TODO: interface/abstract
        public void DeleteItem(Folder selectedItem)
        {
            var subfolders = FlattenSubfolders(selectedItem);
            _dataContext.RemoveRange(subfolders);
            _dataContext.Remove(selectedItem);
            SaveChanges();
        }

        public IEnumerable<Folder> FlattenSubfolders(Folder root)
        {
            var stack = new Stack<Folder>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var currentFolder = stack.Pop();

                yield return currentFolder;

                foreach (var subfolder in currentFolder.Subfolders)
                {
                    stack.Push(subfolder);
                }
            }
        }
    }
}
