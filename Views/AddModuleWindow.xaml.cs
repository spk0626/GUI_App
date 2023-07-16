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
    public partial class AddModuleWindow : Window
    {
        public AddModuleWindow(CalcGpaVM vm)
        {
            InitializeComponent();
            DataContext = vm;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
