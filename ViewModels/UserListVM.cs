using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GUI_Project.Models;
using GUI_Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_Project.ViewModels
{
    public partial class UserListVM : ObservableObject
    {
        public Action CloseAction { get; internal set; }


        [ObservableProperty]
        public ObservableCollection<User> users;

        [ObservableProperty]
        public User selectedUser = null;


        public UserListVM()

        {
            users = new ObservableCollection<User>(getUsers());

        }

        public static List<User> getUsers()
        {
            using (var db = new UserContext())
            {
                return db.Users.OrderBy(u => u.Username).ToList();
            }
        }

        public void RefreshUsers()
        {
            users.Clear();
            foreach (var user in getUsers())
            {
                users.Add(user);
            }
        }



        [RelayCommand]

        public void OpenAddUser()
        {
            var vm = new UserOperationsVM();
            vm.title = "Add User Window";
            UserOperationsWindow window = new UserOperationsWindow(vm);

            var userListWindows = Application.Current.Windows.OfType<UserListWindow>().Where(w => w.IsActive);
            foreach (var userListWindow in userListWindows)
            {
                userListWindow.Hide();
            }

            bool? dialogResult = window.ShowDialog();
            if (dialogResult == true && vm.updatedUser.Username != null)
            {
                users.Add(vm.updatedUser);
            }

            window.Closed += (s, e) =>
            {
                Application.Current.MainWindow.ShowDialog();
            };


        }



        [RelayCommand]

        public void OpenUpdateUser()
        {
            if (selectedUser != null)
            {
                var vm = new UserOperationsVM(selectedUser);
                vm.title = "EDIT STUDENT";
                var window = new UserOperationsWindow(vm);

                var userListWindows = Application.Current.Windows.OfType<UserListWindow>().Where(w => w.IsActive);
                foreach (var userListWindow in userListWindows)
                {
                    userListWindow.Hide();
                }

                window.ShowDialog();

                if (vm.updatedUser != null && users.Contains(selectedUser))
                {
                    int index = users.IndexOf(selectedUser);
                    users.RemoveAt(index);
                    users.Insert(index, vm.updatedUser);
                }

            }
            else
            {
                MessageBox.Show("Please select a user to edit", "Error");
            }
        }

        [RelayCommand]
        public void Delete()
        {
            if (selectedUser != null)
            {

                using (var db = new UserContext())
                {
                    var userToDelete = db.Users.Find(selectedUser.Id);
                    if (userToDelete != null)
                    {
                        db.Users.Remove(userToDelete);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Cannot find user with ID {selectedUser.Id}.", "Error");
                    }

                    users.Remove(selectedUser);

                    MessageBox.Show("User is deleted successfully.", "DELETED");

                }
            }
            else
            {
                MessageBox.Show("Please Select the User before Deleting.", "Error");
            }
        }

        [RelayCommand]

        public void Logout()
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            var userListWindows = Application.Current.Windows.OfType<UserListWindow>().Where(w => w.IsActive);
            foreach (var userListWindow in userListWindows)
            {
                userListWindow.Hide();
            }

            if (mainWindow != null)
            {
                mainWindow.Show();
                var mainWindowVM = mainWindow.DataContext as MainWindowVM;
                if (mainWindowVM != null)
                {
                    mainWindowVM.Username = "";
                    mainWindowVM.Password = "";
                }
            }
            else
            {

                mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }

        }

    }

}
