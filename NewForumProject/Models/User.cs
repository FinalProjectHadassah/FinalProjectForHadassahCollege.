using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForumProject.Models
{
    //test commit 2
    public class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public int UserID { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }

        public String FirstName { get; set; }
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