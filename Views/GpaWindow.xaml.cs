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
    /// Interaction logic for GpaWindow.xaml
    /// </summary>
    public partial class GpaWindow : Window
    {
        public GpaWindow()
        {
        }

        public GpaWindow(CalcGpaVM vm)
        {
            InitializeComponent();
            DataContext = vm;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void CloseUser_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
