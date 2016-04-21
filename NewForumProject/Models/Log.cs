using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Log : Entity
	{
	    public int LogID { get; set; }
        //[int] IDENTITY(1,1) NOT NULL,
        [Required]
	    public DateTime EventDateTime { get; set; }
        //[EventDateTime] [datetime] NOT NULL
        [Required]
        public string EventLevel { get; set; }
        //[EventLevel] [nvarchar](100) NOT NULL
        [Required]
        public string UserName { get; set; }
        //[UserName] [nvarchar](100) NOT NULL
        [Required]
        public string MachineName { get; set; }
        //[MachineName] [nvarchar](100) NOT NULL
        [Required]
        public string EventMessage { get; set; }
        //[EventMessage] [nvarchar](max) NOT NULL
        public string ErrorSource { get; set; }
        //[ErrorSource] [nvarchar](100) NULL
        public string ErrorClass { get; set; }
        //[ErrorClass]  [nvarchar](500) NULL
        public string ErrorMethod { get; set; }
        //[ErrorMethod] [nvarchar](max) NULL
        public string ErrorMessage { get; set; }
        //[ErrorMessage] [nvarchar](max) NULL
        public string InnerErrorMessage { get; set; }
        //[InnerErrorMessage] [nvarchar](max) NULL
    }
}