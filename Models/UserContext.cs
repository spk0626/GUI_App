using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GUI_Project.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        //public string path = @"C:\\Users\\ACER\\Desktop\\GUI\\GUI_Project\\GUI_Project\\user_database.db";
        public string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_database.db");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

    }
}
