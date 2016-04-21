using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    public class Academy : Entity
    {
        public Academy()
        {
        }

        public int AcademyID { get; set; }

        public string AcademyName { get; set; }
    }
}