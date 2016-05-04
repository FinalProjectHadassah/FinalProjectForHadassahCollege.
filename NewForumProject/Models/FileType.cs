
using System.ComponentModel;

namespace NewForumProject.Models
{
    public enum FileType
    {
        Avatar, Photo
    }

    public enum ImageFormat
    {
        bmp,
        jpeg,
        gif,
        tiff,
        png,
        unknown
    }

    public enum TypeOfLecture
    {
        [Description("מצגת")]
        Presentation,
        [Description("Lecture")]
        Lecture,
        [Description("Lab")]
        Lab,
        [Description("Seminar")]
        Seminar,
        [Description("Practice")]
        Practice
    }

}