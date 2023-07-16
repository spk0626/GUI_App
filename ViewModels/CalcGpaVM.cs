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
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI_Project.ViewModels
{
    public partial class CalcGpaVM : ObservableObject
    {
        [ObservableProperty]
        public string moduleCode;

        [ObservableProperty]
        public int credits;

        [ObservableProperty]
        public int marks;

        [ObservableProperty]
        public double gPA;

        [ObservableProperty]
        public char grade;

        public Module selectedModule { get; private set; }

        public ObservableCollection<Module> modules { get; } = new ObservableCollection<Module>();


        public CalcGpaVM(Module m, AddStudentVM addStudentVM)
        {
            selectedModule = m;
            moduleCode = selectedModule.ModuleCode;
            credits = selectedModule.Credits;
            marks = selectedModule.Marks;
            var updatedStudent = addStudentVM.updatedStudent;
            if (updatedStudent != null)
            {
                updatedStudent.GPA = gPA;
            }

        }

        public CalcGpaVM()
        {
        }

        [RelayCommand]
        public void Add()
        {
            var module = new Module
            {
                ModuleCode = "Module Code",
                Credits = 0,
                Marks = 0,
                Grade = '\0'
            };

            modules.Add(module);
        }

        [RelayCommand]
        public void AddModule()
        {
      
              var vm = new CalcGpaVM();

                AddModuleWindow window = new AddModuleWindow(vm);

                var gpaWindows = Application.Current.Windows.OfType<GpaWindow>().Where(w => w.IsActive);
                foreach (var gpaWindow in gpaWindows)
                {
                    gpaWindow.Hide();
                }

                window.ShowDialog();

        }


        [RelayCommand]
        public void GetGrades(int marks)
        {
                marks = selectedModule.Marks;
                char grade = '\0';

                if (Marks > 0)
                {
                    if (Marks >= 75)
                        grade = 'A';
                    else if (Marks >= 65)
                        grade = 'B';
                    else if (Marks >= 55)
                        grade = 'C';
                    else if (Marks <= 35)
                        grade = 'E';
                }
                else
                    MessageBox.Show("Invalid Marks. Please check again", "Error");

                selectedModule.Grade = grade;
                MessageBox.Show("Grades are displayed in the list");  
        }

        [RelayCommand]
        public void CalculateGPA(List<Module> modules)
        {
            double totalGradePoints = 0;
            int totalCredits = 0;

            foreach (var module in modules)
            {
                int marks = module.Marks;
                char grade = module.Grade;
                double gradePoints = 0;

                if (marks > 0)
                {
                    if (grade == 'A')
                        gradePoints = 4.0;
                    else if (grade == 'B')
                        gradePoints = 3.0;
                    else if (grade == 'C')
                        gradePoints = 2.0;
                    else if (grade == 'E')
                        gradePoints = 0;
                }
                else
                {
                    MessageBox.Show("Invalid Marks. Please check again", "Error");
                }

                totalGradePoints += gradePoints * module.Credits;
                totalCredits += module.Credits;
            }

            double GPA = totalGradePoints / totalCredits;
            GPA = gPA;

        }

        [RelayCommand]
        public void BackToGpa()
        {
            var gpaWindow = Application.Current.Windows.OfType<GpaWindow>().FirstOrDefault();

            var addModuleWindows = Application.Current.Windows.OfType<AddModuleWindow>().Where(w => w.IsActive);
            foreach (var addModuleWindow in addModuleWindows)
            {
                addModuleWindow.Hide();
            }

            if (gpaWindow != null)
            {
                gpaWindow.Show();
            }
            else
            {

                gpaWindow = new GpaWindow();
                gpaWindow.ShowDialog();
            }
        }

        [RelayCommand]
        public void Back()
        {
            var addStudentWindow = Application.Current.Windows.OfType<AddStudentWindow>().FirstOrDefault();

            var gpaWindows = Application.Current.Windows.OfType<GpaWindow>().Where(w => w.IsActive);
            foreach (var gpaWindow in gpaWindows)
            {
                gpaWindow.Hide();
            }

            if (addStudentWindow != null)
            {
                addStudentWindow.Show();
            }
            else
            {

                addStudentWindow = new AddStudentWindow();
                addStudentWindow.ShowDialog();
            }
        }



    }
}



