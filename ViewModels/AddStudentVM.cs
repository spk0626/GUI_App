using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GUI_Project.Views;
using System.Windows.Input;
using GUI_Project.Models;

namespace GUI_Project.ViewModels
{
    public partial class AddStudentVM : ObservableObject
    {
        [ObservableProperty]
        public string title;

        public Action CloseAction { get; internal set; }

        public Student updatedStudent { get; private set; }

        private bool isSaved;


        [ObservableProperty]
        public string regNo;

        [ObservableProperty]
        public string fullName;

        [ObservableProperty]
        public string department;

        [ObservableProperty]
        public double gPA;


        public AddStudentVM() { }

        public AddStudentVM(Student s)
        {
            updatedStudent = s;
            regNo = updatedStudent.RegNo;
            fullName = updatedStudent.FullName;
            department = updatedStudent.Department;
            gPA = updatedStudent.GPA;
            
        }

       
        public void AddStudents(Student nStudent)
        {
            using (var db = new StudentContext())
            {
                db.Students.Add(nStudent);
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


            if (string.IsNullOrEmpty(regNo) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(department))
            {
                MessageBox.Show("Please fill in all the fields", "Error");
            }
            else
            {
                if (updatedStudent == null)
                {
                    updatedStudent = new Student()
                    {
                        RegNo = regNo,
                        FullName = fullName,
                        Department = department,
                        GPA = gPA

                    };


                    AddStudents(updatedStudent);
                    MessageBox.Show("Student is Successfully Added", "Message");

                    regNo = "";
                    fullName = "";
                    department = "";
                    gPA = 0;

                    IsSaved = true;
                }
                else
                {

                    updatedStudent.RegNo = regNo;
                    updatedStudent.FullName = fullName;
                    updatedStudent.Department = department;
                    updatedStudent.GPA = gPA;


                    using (var db = new StudentContext())
                    {
                        var recordToUpdate = db.Students.FirstOrDefault(r => r.RegNo == updatedStudent.RegNo);
                        if (recordToUpdate != null)
                        {
                            recordToUpdate.RegNo = updatedStudent.RegNo;
                            recordToUpdate.FullName = updatedStudent.FullName;
                            recordToUpdate.Department = updatedStudent.Department;
                            recordToUpdate.GPA = updatedStudent.GPA;
                            db.SaveChanges();
                        }



                    }

                    MessageBox.Show("Student is successfully updated", "Message");

                    IsSaved = true;

                }


            }
        }

        [RelayCommand]
        public void GpaCal()
        {
            var vm = new CalcGpaVM();

            GpaWindow window = new GpaWindow(vm);

            var addStudentWindows = Application.Current.Windows.OfType<AddStudentWindow>().Where(w => w.IsActive);
            foreach (var addStudentWindow in addStudentWindows)
            {
                addStudentWindow.Hide();
            }

            window.ShowDialog();
        }

        [RelayCommand]

        public void GoBack()
        {

            var addStudentWindows = Application.Current.Windows.OfType<AddStudentWindow>().Where(w => w.IsActive);
            foreach (var addStudentWindow in addStudentWindows)
            {
                var addStudentVM = addStudentWindow.DataContext as AddStudentVM;
                if (!addStudentVM.IsSaved)
                {
                    var result = MessageBox.Show("Do you want to save the entered details?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        addStudentVM.Save();
                    }
                }
                addStudentWindow.Hide();
            }

            var studentListWindow = Application.Current.Windows.OfType<StudentListWindow>().FirstOrDefault();
            if (studentListWindow != null)
            {
                studentListWindow.Show();
                var studentListVM = studentListWindow.DataContext as StudentListVM;
                studentListVM.RefreshStudents();
            }
            else
            {

                studentListWindow = new StudentListWindow();
                studentListWindow.ShowDialog();
            }

        }

    }
}
