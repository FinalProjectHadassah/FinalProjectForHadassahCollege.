﻿using NewForumProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NewForumProject.Repositories
{

    using NewForumProject.DAL;
    using NewForumProject.Interfaces;
    using System.Data.Entity;

    public class DataContextRepository : IDataContextRepository
    {
        private DataContext db;

        public DataContextRepository(DataContext _db)
        {
            db = _db;
        }
        //================USERS=========================
        public IEnumerable<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public bool AddUser(User user)
        {
            try
            {
                this.db.Users.Add(user);
                this.db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public User GetUserById(int userID)
        {
            return this.db.Users.Find(userID);
        }

        public FileContentResult GetUserPicById(int userID)
        {
            var picture = db.Pictures.Where(x => x.UserId == userID).FirstOrDefault();
            return new FileContentResult(picture.Content, "image/jpeg");
        }

        public IEnumerable<Subject> GetUserSubjectsById(int id)
        {
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            return user.Subjects.ToList();
        }

        public void SignUserToSubject(Subject subject, int id)
        {
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            user.Subjects.Add(subject);
        }

        public bool SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        //================BLOCKS=========================
        public IEnumerable<Block> GetBlocks()
        {
            return db.Blocks.ToList();
        }

        //================ACADEMIES=========================
        public IEnumerable<Academy> GetAllAcademies()
        {
            return this.db.Academies;
        }

        public Academy GetAcademyById(int academyId)
        {
            return this.db.Academies.Find(academyId);
        }

        public bool EditUser(User user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public Academy Get(int id)
        {
            return db.Academies.FirstOrDefault(x => x.AcademyID == id);
        }

        public void Save(Academy entity)
        {
            db.Academies.Attach(entity);
        }

        public void Delete(Academy entity)
        {
            db.Academies.Remove(entity);
        }

        public IEnumerable<Academy> FindAll()
        {
            return db.Academies.ToList();
        }

        public IEnumerable<Academy> Find(string text)
        {
            return db.Academies.Where(x => x.AcademyName.StartsWith(text)).ToList();
        }
        //================SUBJECTS=========================
        public IEnumerable<Subject> FindSubjectByName(string text)
        {
            return db.Subjects.Where(x => x.SubjectName.StartsWith(text)).ToList();
        }

        public Subject GetSubjectById(int id)
        {
            return db.Subjects.FirstOrDefault(x => x.SubjectID == id);
        }

        public IEnumerable<Subject> getSubjectsByAcademyId(int academyId)
        {
            return db.Subjects.Where(x => x.AcademyID == academyId).ToList();
        }

        //can return NULL List, if there is no academy with given name

        public IEnumerable<Subject> getSubjectsByAcademyName(string text)
        {
            IEnumerable<Subject> subjects = null;
            IEnumerable<Academy> academies = Find(text);
            foreach (var academy in academies)
            {
                subjects.Concat(getSubjectsByAcademyId(academy.AcademyID));
            }
            return subjects;

        }

        public void Save(Subject entity)
        {
            db.Subjects.Attach(entity);
        }

        public void Delete(Subject entity)
        {
            db.Subjects.Remove(entity);
        }
        //================POSTS=========================
        public IEnumerable<Post> getPostsByUser(int userId)
        {
            return db.Posts.Where(x => x.User.UserID == userId).ToList();
        }

        public IEnumerable<Post> getPostsByTopic(int topicId)
        {
            return db.Posts.Where(x => x.Topic.TopicID == topicId).ToList();
        }

        public void Save(Post entity)
        {
            db.Posts.Attach(entity);
        }

        public void Delete(Post entity)
        {
            db.Posts.Remove(entity);
        }

        public Post GetPostById(int id)
        {
            return db.Posts.FirstOrDefault(x => x.PostID == id);
        }
        //???
        public string GetPostContent(int id)
        {
            return GetPostById(id).PostContent;
        }
        //Important for project algorithms
        public DateTime GetPostDateCreated(int id)
        {
            return GetPostById(id).DateCreated;
        }

        public DateTime GetDateEdited(int id)
        {
            return GetPostById(id).DateEdited;
        }
        //================FAVORITES=========================
        public Favorite GetFavoriteById(int id)
        {
            return db.Favorites.FirstOrDefault(x => x.FavoriteID == id);
        }

        public IEnumerable<Favorite> GetFavoriteByTopic(int id)
        {
            return db.Favorites.Where(x => x.TopicID == id).ToList();
        }
        public IEnumerable<Favorite> GetFavoriteByUser(int id)
        {
            return db.Favorites.Where(x => x.User.UserID == id).ToList();
        }
        //Favorite GetFavoriteByPost(int id);
        public void Save(Favorite entity)
        {
            db.Favorites.Attach(entity);
        }

        public void Delete(Favorite entity)
        {
            db.Favorites.Remove(entity);
        }

        public IEnumerable<User> SearchUsers(string Name)
        {
            return string.IsNullOrEmpty(Name) ? this.db.Users.ToList() : this.db.Users.Where(x => x.FirstName.StartsWith(Name) || x.LastName.StartsWith(Name)).ToList();
        }

        public bool SavePicture(Picture avatar, int UserID)
        {
            try
            {
                db.Pictures.Add(avatar);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public byte[] GetPhoto(int UserId)//add to interface
        {
            byte[] photo = db
            .Pictures
            .Where(p => p.UserId == UserId)
            .Select(img => img.Content)
            .FirstOrDefault();

            return photo;
        }

        public bool DeleteAllAvatarsFromUser(int userId)
        {
            try
            {
                var picToRemove = db.Pictures.Where(e => e.UserId == userId && e.Type == FileType.Avatar).ToList();
                db.Pictures.RemoveRange(picToRemove);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}