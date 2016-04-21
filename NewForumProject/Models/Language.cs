using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Language : Entity
    {
        public Language()
        {
        }
        public int LanguageID { get; set; }

        [Required]
        [Display(Name = "Language Name")]
        public string Name { get; set; }

        [Required]
        public string LanguageCulture { get; set; }

        [Required]
        public string FlagImageFileName { get; set; }

        [Required]
        public bool RightToLeft { get; set; }
    }
}