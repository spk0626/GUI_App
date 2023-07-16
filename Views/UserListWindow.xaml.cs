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
    /// Interaction logic for UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window
    {
        public UserListWindow()
        {
        }

        public UserListWindow(UserListVM vm)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = vm;
            vm.CloseAction = () => Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logging Out from your account. Use username and password to sign in again.", "Logging out");
            this.Close();
            MainWindow window = new MainWindow();
            window.Show();
        }

        private void CloseUser_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
