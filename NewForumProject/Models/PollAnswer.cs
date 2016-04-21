using System;
using System.Collections.Generic;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PollAnswer : Entity
    {
        public PollAnswer()
        {
            PollVotes = new List<PollVote>();
        }
        public int PollAnswerID { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public int PollID { get; set; }

        [ForeignKey("PollID")]
        public virtual Poll Poll { get; set; }
        public virtual IList<PollVote> PollVotes { get; set; }
    }
}
