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
using System.Windows.Shapes;
using WSIiZ_WPF.Entities;
using WSIiZ_WPF.Services;

namespace WSIiZ_WPF.Views
{
    /// <summary>
    /// Interaction logic for ExamDesignWindow.xaml
    /// </summary>
    public partial class ExamDesignWindow : Window, IActivable
    {
        private readonly ExaminationService _examinationService;

        public ExamDesignWindow(ExaminationService examinationService)
        {
            _examinationService = examinationService;
            InitializeComponent();
        }

        public void Activate(object paramater)
        {
            DataContext = paramater as Exam;
        }
    }
}
