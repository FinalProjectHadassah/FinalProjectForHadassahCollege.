using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewForumProject.Models;

namespace NewForumProject.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UserEditUserViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int AcademyID { get; set; }
    }
}