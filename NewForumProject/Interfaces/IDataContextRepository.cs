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
        bool EditUser(User user);
        IEnumerable<Block> GetBlocks();

        IEnumerable<Subject> GetUserSubjectsById(int id);

        void SignUserToSubject(Subject subject, int id);

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

        Favorite GetFavoriteById(int id);

        IEnumerable<Favorite> GetFavoriteByTopic(int id);

        IEnumerable<Favorite> GetFavoriteByUser(int id);
        //Favorite GetFavoriteByPost(int id);
        void Save(Favorite entity);

        void Delete(Favorite entity);

        IEnumerable<User> SearchUsers(string Name);
    }
}