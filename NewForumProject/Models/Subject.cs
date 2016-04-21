
namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Subject : Entity
    {
        public enum TypeOfLecture { Presentation, Lecture, Lab, Seminar, Practice }
        public int SubjectID { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public TypeOfLecture LectureType { get; set; }

        [Required]
        public bool MustAttend { get; set; }

        [Required]
        public int AcademyID { get; set; }

        [ForeignKey("AcademyID")]
        public virtual Academy Academy { get; set; }
    }
}