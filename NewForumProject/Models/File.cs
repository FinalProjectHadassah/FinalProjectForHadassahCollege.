using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewForumProject.Models
{
    public class File
    {
        public enum FileType { Exam, Slides, Summary, Exercise, HelpMaterial }

        public int FileID { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public FileType Type { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public int SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }


    }
}