namespace NewForumProject.Migrations
{
    using NewForumProject.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NewForumProject.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewForumProject.DAL.DataContext context)
        {

            var academies = new List<Academy>
            {
                new Academy{AcademyName= "Hadassah"},
                new Academy{AcademyName = "JCE"}
            };
            //Fill table from ForumContext with data.
            academies.ForEach(s => context.Academies.Add(s));

            var courses = new List<Subject>
            {
                new Subject{SubjectName= "Chemistry", Academy = academies.First(), LectureType = Subject.TypeOfLecture.Lecture, MustAttend = true},
                new Subject{SubjectName="Microeconomics", Academy = academies.First(), LectureType = Subject.TypeOfLecture.Lab, MustAttend = false},
                new Subject{SubjectName="Macroeconomics", Academy = academies.First(), LectureType = Subject.TypeOfLecture.Presentation, MustAttend = true},
                new Subject{SubjectName="Calculus", Academy = academies.First(), LectureType = Subject.TypeOfLecture.Practice, MustAttend = false},
                new Subject{SubjectName="Trigonometry", Academy = academies.First(), LectureType = Subject.TypeOfLecture.Lecture, MustAttend = true},
                new Subject{SubjectName="Composition", Academy = academies.First(), LectureType = Subject.TypeOfLecture.Seminar, MustAttend = true},
                new Subject{SubjectName="Literature", Academy = academies.First(), LectureType = Subject.TypeOfLecture.Lab, MustAttend = false}
            };
            //Fill table from ForumContext with data.
            courses.ForEach(s => context.Subjects.Add(s));

            Role role1 = new Role { RoleName = "Admin", Description = "Administrator" };
            Role role2 = new Role { RoleName = "User", Description = "Simple User" };
            Password adminPass = new Password("123456", -1361166414);
            Password userPass = new Password("123456", 1361676414);
            User user1 = new User
            {
                Username = "admin",
                Email = "admin@ymail.com",
                FirstName = "Daniel",
                LastName = "Shwarcman",
                Password = adminPass.ComputeSaltedHash(),
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Academy = academies.First(),
                Salt = -1361166414
            };

            User user2 = new User
            {
                Username = "user1",
                Email = "user@ymail.com",
                FirstName = "Vasiliy",
                LastName = "Pupkin",
                Password = userPass.ComputeSaltedHash(),
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Academy = academies.First(),
                Salt = 1361676414
            };

            User user3 = new User
            {
                Username = "user2",
                Email = "user@ymail.com",
                FirstName = "Masha",
                LastName = "Nenasha",
                Password = userPass.ComputeSaltedHash(),
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Academy = academies.First(),
                Salt = 1361676414
            };

            User user4 = new User
            {
                Username = "user3",
                Email = "user@ymail.com",
                FirstName = "Vitaliy",
                LastName = "Genetaliy",
                Password = userPass.ComputeSaltedHash(),
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Academy = academies.First(),
                Salt = 1361676414
            };

            User user5 = new User
            {
                Username = "user4",
                Email = "user@ymail.com",
                FirstName = "Sveta",
                LastName = "Konfeta",
                Password = userPass.ComputeSaltedHash(),
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Academy = academies.First(),
                Salt = 1361676414
            };

            User user6 = new User
            {
                Username = "user5",
                Email = "user@ymail.com",
                FirstName = "Tatiana",
                LastName = "Nesmeyana",
                Password = userPass.ComputeSaltedHash(),
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Academy = academies.First(),
                Salt = 1361676414
            };

            User user7 = new User
            {
                Username = "user6",
                Email = "user@ymail.com",
                FirstName = "Kolya",
                LastName = "Bezjazikov",
                Password = userPass.ComputeSaltedHash(),
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role>(),
                Academy = academies.First(),
                Salt = 1361676414
            };

            user1.Roles.Add(role1);
            user2.Roles.Add(role2);
            user3.Roles.Add(role2);
            user4.Roles.Add(role2);
            user5.Roles.Add(role2);
            user6.Roles.Add(role2);
            user7.Roles.Add(role2);

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);
            context.Users.Add(user5);
            context.Users.Add(user6);
            context.Users.Add(user7);

            Block block = new Block
                              {
                                  Date = DateTime.Now,
                                  BlockedUserID = user2.UserID,
                                  BlockedUser = user2,
                                  BlockerUserID = user1.UserID,
                                  BlockerUser = user1
                              };
            context.Blocks.Add(block);
            context.SaveChanges();
        }
    }
}