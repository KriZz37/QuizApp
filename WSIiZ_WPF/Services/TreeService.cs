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
    /// Responsible for operations performed in TreeWindow TreeView
    /// to the database (folders and quizzes)
    /// </summary>
    public class TreeService : BaseService
    {
        public TreeService(DataContext dataContext) : base(dataContext) { }

        public List<Folder> GetRootFolders()
        {
            // Get whole tree
            return _dataContext.Folders
                .Include(x => x.Quizzes)
                .ThenInclude(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .AsEnumerable()
                .Where(x => x.Parent == null)
                .ToList();
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

        public void AddQuiz(Folder selectedItem, string name)
        {
            _dataContext.Quizzes.Add(
                new Quiz
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
                // It also deletes quizzes with questions and answers (cascade delete)
                _dataContext.RemoveRange(folders);
            }

            if (selectedItem is Quiz)
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
