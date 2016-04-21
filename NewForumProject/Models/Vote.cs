using System;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vote : Entity
    {
        public Vote()
        {

        }
        public int VoteID { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int  UserID { get; set; }

        [Required]
        public int VotedByUserID { get; set; }

        [Required]
        public int PostID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }

        [ForeignKey("VotedByUserID")]
        public virtual User VotedByUser { get; set; }

        public virtual DateTime? DateVoted { get; set; }
    }
}