namespace NewForumProject.Repositories
{
    using NewForumProject.DAL;
    using NewForumProject.Interfaces;
    using NewForumProject.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Get(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserID == id);
        }

        public void Save(User entity)
        {
            _dbContext.Users.Attach(entity);
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
        }

        public IEnumerable<User> FindAll()
        {
            return _dbContext.Users.ToList();
        }

        public IEnumerable<User> Find(string text)
        {
            return _dbContext.Users.Where(x => x.FirstName.StartsWith(text) || x.LastName.StartsWith(text)).ToList();
        }
    }
}