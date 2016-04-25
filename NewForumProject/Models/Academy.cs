using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    using System.ComponentModel;

    public class Academy : Entity
    {
        public Academy()
        {
        }

        public int AcademyID { get; set; }
        [DisplayName("מוסד הלימודים")]
        public string AcademyName { get; set; }
    }
}