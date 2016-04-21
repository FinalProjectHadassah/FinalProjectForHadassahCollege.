using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Topic : Entity
    {
        public Topic()
        {
            Posts = new List<Post>();
            Favorites = new List<Favorite>();
        }
        public int TopicID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public bool Solved { get; set; }
        public bool? SolvedReminderSent { get; set; }

        public string Slug { get; set; }
        public int Views { get; set; }
        public bool IsSticky { get; set; }
        public bool IsLocked { get; set; }

        [Required]
        public int LastPostID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int PollID { get; set; }

        [ForeignKey("LastPostID")]
        public virtual Post LastPost { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("PollID")]
        public virtual Poll Poll { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public virtual IList<Favorite> Favorites { get; set; }
        public bool? Pending { get; set; }
        public int VoteCount
        {
            get { return Posts.Select(x => x.VoteCount).Sum(); }
        }

    }
}