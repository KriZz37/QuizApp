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
using WSIiZ_WPF.Utilities;
using WSIiZ_WPF.Services;
using WSIiZ_WPF.ViewModels;

namespace WSIiZ_WPF.Views
{
    /// <summary>
    /// Interaction logic for TreeWindow.xaml
    /// </summary>
    public partial class TreeWindow : Window
    {
        private readonly TreeViewModel _treeViewModel;
        private readonly ServiceGenerator _serviceGenerator;

        public TreeWindow(
            TreeViewModel treeViewModel,
            ServiceGenerator serviceGenerator)
        {
            _treeViewModel = treeViewModel;
            _serviceGenerator = serviceGenerator;

            DataContext = _treeViewModel;
            InitializeComponent();
        }

        //TODO: Move methods below to ViewModel
        private void OpenExam_TextBlock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var exam = (e.OriginalSource as TextBlock).DataContext as Exam;
            new ExamWindow(exam).Show();
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

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _treeViewModel.SelectedItem = e.NewValue as ITreeItem;
        }

        private void EditExam_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var exam = (sender as MenuItem).DataContext as Exam;
            _serviceGenerator.ShowWindow<ExamDesignWindow>(exam);
        }
    }
}
