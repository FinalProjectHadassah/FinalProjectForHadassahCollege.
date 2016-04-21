using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Poll : Entity
    {
        public Poll()
        {
           PollAnswers = new List<PollAnswer>();
        }

        public int PollID { get; set; }

        [Required]
        public bool IsClosed { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public int? ClosePollAfterDays { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public virtual IList<PollAnswer> PollAnswers { get; set; }
    }
}