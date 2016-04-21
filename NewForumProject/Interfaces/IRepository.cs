using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewForumProject.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey id);
        void Save(TEntity entity);
        void Delete(TEntity entity);
    }
}