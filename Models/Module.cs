using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GUI_Project.Models
{
    public class Module
    {
        public string ModuleCode { get; set; }

        public int Credits { get; set; }

        public int Marks { get; set; }

        public char Grade { get; set; }


        public Module() { }

        public Module(string moduleCode, int credits, int marks, char grade)
        {
            ModuleCode = moduleCode;
            Credits = credits;
            Marks = marks;
            Grade = grade;
        }



    }
}