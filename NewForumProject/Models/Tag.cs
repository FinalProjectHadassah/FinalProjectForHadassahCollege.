
using System.Collections.Generic;

namespace NewForumProject.Models
{
    public class Tag : Entity
    {
        public int TagID { get; set; }

        public string Title { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}
