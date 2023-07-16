using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using GUI_Project.Models;
using GUI_Project.Views;

namespace GUI_Project.ViewModels
{
    public partial class MainWindowVM: ObservableObject
    {

        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string password;

        [ObservableProperty]
        internal string title;

        private UserListVM userListVM;
       
        public User user { get; set; }

        public Action CloseAction { get; internal set; }


        public MainWindowVM()
        {
        }


        public MainWindowVM(User u, UserListVM userListVM)
        {
            user = u;
            user.Username = username;
            user.Password = password;
            this.userListVM = userListVM;


        }

        public bool IsAdmin(string username) 
        {
            bool isAdmin = false;

            if (username.ToLower() == "admin")
            {
                isAdmin = true;
            }
            return isAdmin;

        }

        public bool VerifyAdminPassword(string password)
        {
            bool isPwdCorrect = false;

            using (var db = new UserContext())
            {
                var adminUser = db.Users.FirstOrDefault(u => u.Password == password);
                if (adminUser != null)
                {
                    isPwdCorrect = true;
                }

            }

            return isPwdCorrect;
        }

        public bool VerifyUsername(string username)
        {
            bool isNameCorrect = false;

            using (var db = new UserContext())
            {
                var normalUser = db.Users.FirstOrDefault(u => u.Username == username);
                if (normalUser != null)
                {
                    isNameCorrect = true;
                }
                else
                {
                    MessageBox.Show("Incorrect Username", "Error");
                }
            }

            return isNameCorrect;
        }

        public bool VerifyPassword(string password)
        {
            bool isPwdCorrect = false;

            using (var db = new UserContext())
            {
                var normalUser = db.Users.FirstOrDefault(u => u.Password == password);
                if (normalUser != null)
                {
                    isPwdCorrect = true;
                }
                else
                {
                    MessageBox.Show("Incorrect Password", "Error");
                }
            }

            return isPwdCorrect;
        }


        [RelayCommand]

        public void Login()
        {

            bool IsUserAdmin = IsAdmin(username);
            bool AdPasswordCorrect = VerifyAdminPassword(password);
            bool NormalUsernameCorrect = VerifyUsername(username);
            bool NormaPwdCorrect = VerifyPassword(password);


            if (IsUserAdmin && AdPasswordCorrect)
            {
                var vm = new UserListVM();

                UserListWindow window = new UserListWindow(vm);

                var mainWindows = Application.Current.Windows.OfType<MainWindow>().Where(w => w.IsActive);
                foreach (var mainWindow in mainWindows)
                {
                    mainWindow.Hide();
                }

                window.ShowDialog();
            }
            else if(NormalUsernameCorrect && NormaPwdCorrect)
            {
                var vm = new StudentListVM();

                StudentListWindow window = new StudentListWindow(vm);

                var mainWindows = Application.Current.Windows.OfType<MainWindow>().Where(w => w.IsActive);
                foreach (var mainWindow in mainWindows)
                {
                    mainWindow.Hide();
                }

                window.ShowDialog();


            }
            
        }
    }
}
