using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GUI_Project.ViewModels;

namespace GUI_Project.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string RegNo { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public double GPA { get; set; }

        public Student(string reg, string fullName, string department, double gpa)
        {
            RegNo = reg;
            FullName = fullName;
            Department = department;
            GPA = gpa;

        }

        public Student()
        {

        }

    }
}
