using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UploadFile
    {
        public UploadFile()
        {
            
        }

        public int UploadFileID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}