using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum PostOrderBy
    {
        Standard,

        Newest,

        Votes
    }

    public class Post : Entity
    {
        public Post()
        {
            Votes = new List<Vote>();
            Files = new List<UploadFile>();
            Favorites = new List<Favorite>();
        }

        public int PostID { get; set; }

        [Required]
        public string PostContent { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int VoteCount { get; set; }

        public DateTime DateEdited { get; set; }

        [Required]
        public bool IsSolution { get; set; }

        [Required]
        public bool IsTopicStarter { get; set; }

        public bool? FlaggedAsSpam { get; set; }

        public string IpAddress { get; set; }

        public bool? Pending { get; set; }

        [Required]
        public int TopicID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("TopicID")]
        public virtual Topic Topic { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual IList<Vote> Votes { get; set; }

        public virtual IList<UploadFile> Files { get; set; }

        public virtual IList<Favorite> Favorites { get; set; }
    }
}