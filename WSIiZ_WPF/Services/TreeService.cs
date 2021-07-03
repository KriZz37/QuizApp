using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSIiZ_WPF.Data;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Entities.Interfaces;

namespace WSIiZ_WPF.Services
{
    /// <summary>
    /// Responsible for operations performed in MainWindow TreeView
    /// to the database (folders and exams)
    /// </summary>
    public class TreeService : BaseService
    {
        public TreeService(DataContext dataContext) : base(dataContext) { }

        public IEnumerable<Folder> GetRootFolders()
        {
            // Get whole tree
            return _dataContext.Folders
                .Include(x => x.Exams)
                .AsEnumerable()
                .Where(x => x.Parent == null)
                .ToList();
        }

        public void ChangeTitle(ITreeItem entity, string newTitle)
        {
            entity.Title = newTitle;
            SaveChanges();
        }

        public void AddFolder(Folder selectedFolder, string itemName)
        {
            if (selectedFolder is null)
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
                selectedFolder.Subfolders.Add(
                    new Folder
                    {
                        Parent = selectedFolder,
                        Title = itemName
                    });
            }

            SaveChanges();
        }

        public void AddExam(Folder selectedItem, string name)
        {
            _dataContext.Exams.Add(
                new Exam
                {
                    Title = name,
                    Folder = selectedItem
                });

            SaveChanges();
        }

        public void DeleteItem(ITreeItem selectedItem)
        {
            if (selectedItem is Folder folder)
            {
                var folders = FlattenSubfolders(folder);
                // It also deletes exams (cascade delete)
                _dataContext.RemoveRange(folders);
            }

            if (selectedItem is Exam)
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
