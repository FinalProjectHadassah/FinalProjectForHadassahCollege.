using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForumProject.Models
{
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class Block : Entity
    {
        public int BlockID { get; set; }

        public Block()
        {
        }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Blocker")]
        public int BlockerUserID { get; set; }

        [Display(Name = "Blocked")]
        public int BlockedUserID { get; set; }

        [ForeignKey("BlockerUserID")]
        public virtual User BlockerUser { get; set; }

        [ForeignKey("BlockedUserID")]
        public virtual User BlockedUser { get; set; }
    }
}