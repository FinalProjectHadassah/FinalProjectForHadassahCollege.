namespace NewForumProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class Category : Entity
    {
        public Category()
        {
            this.Topics = new List<Topic>();
        }
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsLocked { get; set; }

        [Required]
        public bool? ModerateTopics { get; set; }

        [Required]
        public bool? ModeratePosts { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public string PageTitle { get; set; }
        public string Path { get; set; }
        public string MetaDescription { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }

        [Required]
        public int ParentCategoryID { get; set; }

        [ForeignKey("ParentCategoryID")]
        public virtual Category ParentCategory { get; set; }
        public virtual IList<Topic> Topics { get; set; }
        public int Level { get; set; }
    }
}