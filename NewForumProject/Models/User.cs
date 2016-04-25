using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForumProject.Models
{
    using System.ComponentModel;

    //test commit 2
    public class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public int UserID { get; set; }

        [Required]
        [DisplayName("שם משתמש")]
        public String Username { get; set; }

        [Required]
        [DisplayName("דואר אלאקטרוני")]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }
        [DisplayName("שם פרטי")]
        public String FirstName { get; set; }
        [DisplayName("שם משפחה")]
        public String LastName { get; set; }

        public Boolean IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public int AcademyID { get; set; }

        public int? Salt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        [ForeignKey("AcademyID")]
        public virtual Academy Academy { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}