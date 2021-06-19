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
            var rootFolders = _treeService.GetRootFolders();
            FolderTreeView.Items.Add(rootFolders);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _dataService.Dispose();
        }

        private void DeleteTreeItem_Button_Click(object sender, RoutedEventArgs e)
        {
            var item = FolderTreeView.SelectedItem as TreeViewItem;
            var itemIsRoot = item.Parent is not TreeViewItem;

            if (itemIsRoot)
                FolderTreeView.Items.Remove(item);
            else
            {
                var itemParent = item.Parent as TreeViewItem;
                itemParent.Items.Remove(item);
            }

            FolderTreeView.Items.Refresh();
        }

        private void AddTreeItem_Button_Click(object sender, RoutedEventArgs e)
        {
            var itemName = newTreeItemName.Text;

            if (string.IsNullOrWhiteSpace(itemName))
            {
                MessageBox.Show("Nazwa nie może być pusta");
                return;
            }

            if (FolderTreeView.SelectedItem is not TreeViewItem selectedItemParent)
                FolderTreeView.Items.Add(new TreeViewItem { Header = itemName });
            else
                selectedItemParent.Items.Add(new TreeViewItem { Header = itemName });

            newTreeItemName.Text = string.Empty;
            FolderTreeView.Items.Refresh();
        }

        private void ChangeTreeItemName_Button_Click(object sender, RoutedEventArgs e)
        {
            var newItemName = changeTreeItemName.Text;

            if (string.IsNullOrWhiteSpace(newItemName))
            {
                MessageBox.Show("Nazwa nie może być pusta");
                return;
            }

            var selectedItem = FolderTreeView.SelectedItem as TreeViewItem;
            selectedItem.Header = newItemName;

            changeTreeItemName.Text = string.Empty;
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
    }
}
