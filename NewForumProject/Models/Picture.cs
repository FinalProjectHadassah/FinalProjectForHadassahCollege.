using System.ComponentModel.DataAnnotations;

namespace NewForumProject.Models
{
    using System.ComponentModel;

    public class Picture : Entity
    {
        public int PictureID { get; set; }
        [StringLength(255)]
        [DisplayName("תמונת פרופיל")]
        public string PictureName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType Type { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}