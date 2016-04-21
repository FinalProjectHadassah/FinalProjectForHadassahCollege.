using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    public class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}