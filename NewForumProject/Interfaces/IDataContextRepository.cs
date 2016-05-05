using NewForumProject.Models;
using System;

namespace NewForumProject.Interfaces
{
    using System.Collections.Generic;
    public interface IDataContextRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userid);
        bool AddUser(User user);
        bool SaveChanges();
        IEnumerable<Academy> GetAllAcademies();
        IEnumerable<Block> GetBlocks();

        IEnumerable<Subject> GetUserSubjectsById(int id);

        bool SignUserToSubject(SearchSubjectViewModel subject, int userId);

        void Save(Academy entity);

        void Delete(Academy entity);

        IEnumerable<Academy> FindAll();

        IEnumerable<Academy> Find(string text);

        IEnumerable<Subject> FindSubjectByName(string text);

        Subject GetSubjectById(int id);

        IEnumerable<Subject> getSubjectsByAcademyId(int academyId);

        IEnumerable<Subject> getSubjectsByAcademyName(string text);

        void Save(Subject entity);

        void Delete(Subject entity);

        IEnumerable<Post> getPostsByUser(int userId);

        IEnumerable<Post> getPostsByTopic(int topicId);

        void Save(Post entity);

        void Delete(Post entity);

        Post GetPostById(int id);
        string GetPostContent(int id);

        DateTime GetPostDateCreated(int id);

        DateTime GetDateEdited(int id);



        IEnumerable<User> SearchUsers(string Name);

        IEnumerable<Subject> GetAllSubjects();
    }
}