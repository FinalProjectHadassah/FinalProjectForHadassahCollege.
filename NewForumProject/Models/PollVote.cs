namespace NewForumProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PollVote : Entity
    {
        public PollVote()
        {
        }
        public int PollVoteID { get; set; }

        [Required]
        public int PollAnswerID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("PollAnswerID")]
        public virtual PollAnswer PollAnswer { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
