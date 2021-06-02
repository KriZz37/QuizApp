using System;
using System.Collections.Generic;
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

namespace WSIiZ_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataContext _dataContext;
        public MainWindow(DataContext dataContext)
        {
            _dataContext = dataContext;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Dummy data
            var item1 = new TreeViewItem { Header = "1" };
            var item11 = new TreeViewItem { Header = "1.1" };
            var item111 = new TreeViewItem { Header = "1.1.1" };
            var item112 = new TreeViewItem { Header = "1.1.2" };
            var item1121 = new TreeViewItem { Header = "1.1.2.1" };
            var item12 = new TreeViewItem { Header = "1.2" };
            var item2 = new TreeViewItem { Header = "2" };
            var item21 = new TreeViewItem { Header = "2.1" };
            var item3 = new TreeViewItem { Header = "3" };

            item112.Items.Add(item1121);
            item11.Items.Add(item112);
            item11.Items.Add(item111);
            item1.Items.Add(item11);
            item1.Items.Add(item12);
            FolderTreeView.Items.Add(item1);
            item2.Items.Add(item21);
            FolderTreeView.Items.Add(item2);
            FolderTreeView.Items.Add(item3);

            // Creates a db file if doesn't exist
            _dataContext.Database.EnsureCreated();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Clean up database connections
            _dataContext.Dispose();
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
                FolderTreeView.Items.Add(new TreeViewItem{Header = itemName});
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
            if (FolderTreeView.SelectedItem is TreeViewItem item)
                item.IsSelected = false;
        }
    }
}
