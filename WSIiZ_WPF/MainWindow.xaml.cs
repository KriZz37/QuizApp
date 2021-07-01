using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using WSIiZ_WPF.Services;

namespace WSIiZ_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataService _dataService;
        private readonly TreeService _treeService;
        public MainWindow(
            DataService dataService,
            TreeService treeService)
        {
            _dataService = dataService;
            _treeService = treeService;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _dataService.EnsureDbCreated();
            BuildTree();
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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _dataService.Dispose();
        }

        private void DeleteTreeItem_Button_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć?\n" +
                "Jeśli to folder, to zostaną usunięte wszystkie pliki wewnątrz!", "Potwierdź usunięcie",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Cancel)
                return;

            var selectedItem = GetSelectedTreeItem();
            if (selectedItem is not null)
            {
                _treeService.DeleteItem(selectedItem);
                BuildTree();
            }
        }

        private void AddTreeItem_Button_Click(object sender, RoutedEventArgs e)
        {
            var itemName = newTreeItemName.Text;

            if (!NameIsValid(itemName))
                return;

            var selectedItem = GetSelectedTreeItem();
            _treeService.AddTreeItem(selectedItem, itemName);
            newTreeItemName.Text = string.Empty;
            BuildTree();
        }

        private void ChangeTreeItemName_Button_Click(object sender, RoutedEventArgs e)
        {
            var newItemName = changeTreeItemName.Text;

            if (!NameIsValid(newItemName))
                return;

            var selectedItem = GetSelectedTreeItem();
            _treeService.ChangeTitle(selectedItem, newItemName);
            changeTreeItemName.Text = string.Empty;
            BuildTree();
        }

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

        // TODO: interface/abstract
        private Folder GetSelectedTreeItem()
        {
            return FolderTreeView.SelectedItem as Folder;
        }
    }
}
