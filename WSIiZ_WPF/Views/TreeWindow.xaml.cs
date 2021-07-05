using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WSIiZ_WPF.Data;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Entities.Interfaces;
using WSIiZ_WPF.Services;

namespace WSIiZ_WPF.Views
{
    /// <summary>
    /// Interaction logic for TreeWindow.xaml
    /// </summary>
    public partial class TreeWindow : Window
    {
        private readonly TreeService _treeService;
        private readonly ServiceGenerator _serviceGenerator;

        public TreeWindow(
            TreeService treeService,
            ServiceGenerator serviceGenerator)
        {
            _treeService = treeService;
            _serviceGenerator = serviceGenerator;

            InitializeComponent();
            BuildTree();
        }

        private void DeleteTreeItem_Button_Click(object sender, RoutedEventArgs e)
        {
            if (FolderTreeView.SelectedItem is not ITreeItem selectedItem)
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

        private void AddTreeFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            var folderName = newTreeFolderName.Text;
            if (!NameIsValid(folderName)) return;

            var selectedFolder = FolderTreeView.SelectedItem as Folder;
            _treeService.AddFolder(selectedFolder, folderName);
            newTreeFolderName.Text = string.Empty;
            BuildTree();
        }

        private void AddExam_Button_Click(object sender, RoutedEventArgs e)
        {
            var examName = newExamName.Text;
            if (!NameIsValid(examName)) return;

            var selectedFolder = FolderTreeView.SelectedItem as Folder;

            if (selectedFolder is not Folder)
            {
                MessageBox.Show("Egzamin musi być dodany do folderu", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _treeService.AddExam(selectedFolder, examName);
            newExamName.Text = string.Empty;
            BuildTree();
        }

        private void ChangeTreeItemName_Button_Click(object sender, RoutedEventArgs e)
        {
            var newItemName = changeTreeItemName.Text;
            if (!NameIsValid(newItemName)) return;

            if (FolderTreeView.SelectedItem is not ITreeItem selectedItem)
            {
                MessageBox.Show("Wybierz element, któremu chcesz zmienić nazwę", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _treeService.ChangeTitle(selectedItem, newItemName);
            changeTreeItemName.Text = string.Empty;
            BuildTree();
        }

        private void OpenExamDesign_TextBlock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var exam = (e.OriginalSource as TextBlock).DataContext as Exam;
            _serviceGenerator.ShowWindow<ExamDesignWindow>(exam);
        }

        // TODO: Przenoszenie folderów/egzaminów
        // TODO: Zwijanie po wykonaniu akcji na drzewie

        private void DeselectItem_TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FolderTreeView.SelectedItem is not null)
            {
                // Use tag, because tree items aren't TreeVievItems
                var selectedTVI = FolderTreeView.Tag as TreeViewItem;
                selectedTVI.IsSelected = false;
            }
        }

        private void AddTag_TreeView_Selected(object sender, RoutedEventArgs e)
        {
            FolderTreeView.Tag = e.OriginalSource;
        }

        private bool NameIsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Nazwa nie może być pusta");
                return false;
            }

            return true;
        }

        private void BuildTree()
        {
            // Clear existing items when it's a refresh
            FolderTreeView.Items.Clear();

            var rootFolders = _treeService.GetRootFolders();

            foreach (var folder in rootFolders)
            {
                FolderTreeView.Items.Add(folder);
            }
        }
    }
}
