

namespace NewForumProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Favorite : Entity
    {
        public Favorite()
        {
        }
        public int FavoriteID { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Post ID")]
        public int PostID { get; set; }

        [Required]
        [Display(Name = "Topic ID")]
        public int TopicID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }

        [ForeignKey("TopicID")]
        public virtual Topic Topic { get; set; }
    }
}