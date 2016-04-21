using System;

namespace NewForumProject.Models
{
    public class Vote : Entity
    {
        public Vote()
        {

        }
        public int VoteID { get; set; }
        public int Amount { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public virtual User VotedByUser { get; set; }
        public virtual DateTime? DateVoted { get; set; }
    }
}