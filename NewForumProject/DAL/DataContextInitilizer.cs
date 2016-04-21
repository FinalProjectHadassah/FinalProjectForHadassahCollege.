using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NewForumProject.Models;

namespace NewForumProject.DAL
{
    public class DataContextInitilizer : CreateDatabaseIfNotExists<DataContext>
    {
    }
}

////USE master;
////GO
////ALTER DATABASE NewForumProject SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
////GO
////DROP DATABASE NewForumProject
////DELETE FROM dbo.Academies
////DELETE FROM dbo.UserRoles
////DELETE FROM dbo.Subjects
////DELETE FROM dbo.Users
////DELETE FROM dbo.Roles