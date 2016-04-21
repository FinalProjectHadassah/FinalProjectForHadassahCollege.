using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}