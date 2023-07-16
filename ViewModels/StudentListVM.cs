using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GUI_Project.Models;
using GUI_Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


namespace GUI_Project.ViewModels
{
    public partial class StudentListVM : ObservableObject
    {
        public Action CloseAction { get; internal set; }

        [ObservableProperty]
        public ObservableCollection<Student> students;

        [ObservableProperty]
        public Student selectedStudent = null;


        public StudentListVM()

        {
            students = new ObservableCollection<Student>(getStudents());

        }

        public static List<Student> getStudents()
        {
            using (var db = new StudentContext())
            {
                return db.Students.OrderBy(s => s.RegNo).ToList();
            }
        }

        public void RefreshStudents()
        {
            students.Clear();
            foreach (var student in getStudents())
            {
                students.Add(student);
            }
        }

        [RelayCommand]
        public void Logout()
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            var studentListWindows = Application.Current.Windows.OfType<StudentListWindow>().Where(w => w.IsActive);
            foreach (var studentListWindow in studentListWindows)
            {
                studentListWindow.Hide();
            }

            if (mainWindow != null)
            {
                mainWindow.Show();
                var mainWindowVM = mainWindow.DataContext as MainWindowVM;
                if (mainWindowVM != null)
                {
                    mainWindowVM.username = "";
                    mainWindowVM.password = "";
                }
            }
            else
            {

                mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
        }

        [RelayCommand]
        public void AddStudent()
        {
            var vm = new AddStudentVM();
            vm.title = "Add New Student";
            AddStudentWindow window = new AddStudentWindow(vm);

            var studentListWindows = Application.Current.Windows.OfType<StudentListWindow>().Where(w => w.IsActive);
            foreach (var studentListWindow in studentListWindows)
            {
                studentListWindow.Hide();
            }

            bool? dialogResult = window.ShowDialog();
            if (dialogResult == true && vm.updatedStudent.RegNo != null)
            {
                students.Add(vm.updatedStudent);
            }

            window.Closed += (s, e) =>
            {
                Application.Current.MainWindow.ShowDialog();
            };

        }


        [RelayCommand]

        public void EditStudent()
        {
            if (selectedStudent != null)
            {
                var vm = new AddStudentVM(selectedStudent);
                vm.title = "EDIT STUDENT";
                var window = new AddStudentWindow(vm);

                var studentListWindows = Application.Current.Windows.OfType<StudentListWindow>().Where(w => w.IsActive);
                foreach (var studentListWindow in studentListWindows)
                {
                    studentListWindow.Hide();
                }

                window.ShowDialog();

                if (vm.updatedStudent != null && students.Contains(selectedStudent))
                {
                    int index = students.IndexOf(selectedStudent);
                    students.RemoveAt(index);
                    students.Insert(index, vm.updatedStudent);
                }

            }
            else
            {
                MessageBox.Show("Please select a student to edit", "Error");
            }
        }

        [RelayCommand]
        public void Delete()
        {
            if (selectedStudent != null)
            {

                using (var db = new StudentContext())
                {
                    var studentToDelete = db.Students.Find(selectedStudent.RegNo);
                    if (studentToDelete != null)
                    {
                        db.Students.Remove(studentToDelete);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Cannot find student with ID {selectedStudent.RegNo}.", "Error");
                    }

                    students.Remove(selectedStudent);

                    MessageBox.Show("Student is deleted successfully.", "DELETED");

                }
            }
            else
            {
                MessageBox.Show("Please Select the Student before Deleting.", "Error");
            }
        }

    }
}
