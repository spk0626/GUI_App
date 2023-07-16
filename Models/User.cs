using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GUI_Project.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }



        public User()
        {
        }

        public User(string fullName, string username, string password)
        {
            FullName = fullName;
            Username = username;
            Password = password;
        }


    }
}
