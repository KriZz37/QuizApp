using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuizApp.Entities;
using QuizApp.Entities.Interfaces;
using QuizApp.Services;
using QuizApp.Utilities;
using QuizApp.Views;

namespace QuizApp.ViewModels
{
    /// <summary>
    /// TreeWindow ViewModel
    /// </summary>
    public class TreeViewModel : BaseViewModel
    {
        private readonly TreeService _treeService;

        public TreeViewModel(
            TreeService treeService)
        {
            _treeService = treeService;

            DeleteTreeItemCmd = new RelayCommand(c => DeleteTreeItem());
            AddTreeFolderCmd = new RelayCommand(c => AddTreeFolder());
            AddQuizCmd = new RelayCommand(c => AddQuiz());
            ChangeTreeItemNameCmd = new RelayCommand(c => ChangeTreeItemName());

            BuildTree();
        }

        // Commands
        public ICommand DeleteTreeItemCmd { get; set; }
        public ICommand AddTreeFolderCmd { get; set; }
        public ICommand AddQuizCmd { get; set; }
        public ICommand ChangeTreeItemNameCmd { get; set; }

        // View properties
        private string _newFolderName;
        public string NewFolderName
        {
            get => _newFolderName;
            set => OnPropertyChanged(ref _newFolderName, value);
        }

        private string _newQuizName;
        public string NewQuizName
        {
            get => _newQuizName;
            set => OnPropertyChanged(ref _newQuizName, value);
        }

        private string _treeItemNameToChange;
        public string TreeItemNameToChange
        {
            get => _treeItemNameToChange;
            set => OnPropertyChanged(ref _treeItemNameToChange, value);
        }

        private List<Folder> _treeFolders = new();
        public List<Folder> TreeFolders
        {
            get => _treeFolders;
            set => OnPropertyChanged(ref _treeFolders, value);
        }

        public ITreeItem SelectedItem { get; set; }

        // Command methods
        private void DeleteTreeItem()
        {
            if (SelectedItem is not ITreeItem selectedItem)
            {
                MessageBox.Show("Wybierz element, który chcesz usunąć", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var messageBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć?\n" +
                "Jeśli to folder, to zostaną usunięte wszystkie pliki wewnątrz!", "Potwierdź usunięcie",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Cancel) return;

            _treeService.DeleteItem(selectedItem);
            BuildTree();
        }

        private void AddTreeFolder()
        {
            if (!NameIsValid(NewFolderName)) return;

            var selectedFolder = SelectedItem as Folder;
            _treeService.AddFolder(selectedFolder, NewFolderName);
            NewFolderName = string.Empty;
            BuildTree();
        }

        private void AddQuiz()
        {
            if (!NameIsValid(NewQuizName)) return;

            var selectedFolder = SelectedItem as Folder;

            if (SelectedItem is not Folder)
            {
                MessageBox.Show("Quiz musi być dodany do folderu", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _treeService.AddQuiz(selectedFolder, NewQuizName);
            NewQuizName = string.Empty;
            BuildTree();
        }

        private void ChangeTreeItemName()
        {
            if (!NameIsValid(TreeItemNameToChange)) return;

            if (SelectedItem is not ITreeItem selectedItem)
            {
                MessageBox.Show("Wybierz element, któremu chcesz zmienić nazwę", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _treeService.ChangeTitle(selectedItem, TreeItemNameToChange);
            TreeItemNameToChange = string.Empty;
            BuildTree();
        }

        private void BuildTree()
        {
            TreeFolders = _treeService.GetRootFolders();
        }
    }
}
