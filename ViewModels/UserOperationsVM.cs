using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GUI_Project.Models;
using GUI_Project.Views;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Windows;

namespace GUI_Project.ViewModels
{
    public partial class UserOperationsVM : ObservableObject
    {
        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string fullName;

        public User updatedUser { get; private set; }

        private bool isSaved;

        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string password;


        public Action CloseAction { get; internal set; }

        public UserOperationsVM()
        {

        }

        public UserOperationsVM(User u)
        {
            updatedUser = u;
            fullName = updatedUser.FullName;
            username = updatedUser.Username;
            password = updatedUser.Password;
        }



        public void AddUsers(User nUser)
        {
            using (var db = new UserContext())
            {
                db.Users.Add(nUser);
                db.SaveChanges();
            }
        }

        public bool IsSaved
        {
            get { return isSaved; }
            set
            {
                isSaved = value;
                OnPropertyChanged(nameof(IsSaved));
            }
        }

        [RelayCommand]
        public void Save()
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all the fields", "Error");
            }
            else
            {
                if (updatedUser == null)
                {
                    updatedUser = new User()
                    {
                        FullName = fullName,
                        Username = username,
                        Password = password
                    };


                    AddUsers(updatedUser);
                    MessageBox.Show("User is Successfully Added", "Message");

                    fullName = "";
                    username = "";
                    password = "";

                    IsSaved = true;
                }
                else
                {
                    updatedUser.FullName = fullName;
                    updatedUser.Username = username;
                    updatedUser.Password = password;


                    using (var db = new UserContext())
                    {
                        var recordToUpdate = db.Users.FirstOrDefault(r => r.Id == updatedUser.Id);
                        if (recordToUpdate != null)
                        {
                            recordToUpdate.FullName = updatedUser.FullName;
                            recordToUpdate.Username = updatedUser.Username;
                            recordToUpdate.Password = updatedUser.Password;
                            db.SaveChanges();
                        }
                    }

                    MessageBox.Show("User is successfully updated", "Message");

                    IsSaved = true;

                }


            }
        }

        [RelayCommand]

        public void Back()
        {
            var userOperationsWindows = Application.Current.Windows.OfType<UserOperationsWindow>().Where(w => w.IsActive);
            foreach (var userOperationsWindow in userOperationsWindows)
            {
                var userOperationsVM = userOperationsWindow.DataContext as UserOperationsVM;
                if (!userOperationsVM.IsSaved)
                {
                    var result = MessageBox.Show("Do you want to save the entered details?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        userOperationsVM.Save();
                    }
                    
                }
                userOperationsWindow.Close();
            }

            var userListWindow = Application.Current.Windows.OfType<UserListWindow>().FirstOrDefault();
            if (userListWindow != null)
            {
                userListWindow.Show();
                var userListVM = userListWindow.DataContext as UserListVM;
                userListVM.RefreshUsers();
            }
            else
            {
                userListWindow = new UserListWindow();
                userListWindow.ShowDialog();
            }
            
        }

    }
}
