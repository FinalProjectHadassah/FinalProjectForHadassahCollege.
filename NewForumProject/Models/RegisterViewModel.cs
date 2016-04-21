using System;
using System.ComponentModel.DataAnnotations;

namespace NewForumProject.Models
{
    using System.Linq;

    using NewForumProject.DAL;
    using System.Collections.Generic;

    public class RegisterViewModel
    {
        public RegisterViewModel()
        {

        }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday Date")]
        public DateTime Birthday { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Academy")]
        public int AcademyID { get; set; }
    }
}