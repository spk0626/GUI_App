using GUI_Project.ViewModels;
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

namespace GUI_Project.Views
{
    /// <summary>
    /// Interaction logic for StudentListWindow.xaml
    /// </summary>
    public partial class StudentListWindow : Window
    {

        public StudentListWindow() { }

        public StudentListWindow(StudentListVM vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.CloseAction = () => Close();
        }

        private void CloseUser_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
