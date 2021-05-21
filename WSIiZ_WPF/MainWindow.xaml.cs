using System;
using System.Collections.Generic;
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

namespace WSIiZ_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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

        }
    }
}
