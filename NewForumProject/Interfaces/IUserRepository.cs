using NewForumProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Interfaces
{
    public interface IUserRepository : IRepository<User, int>
    {
        IEnumerable<User> FindAll();
        IEnumerable<User> Find(string text);
    }
}