namespace NewForumProject.Repositories
{
    using NewForumProject.DAL;
    using NewForumProject.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public EntityFrameworkUnitOfWork(DataContext context)
        {
            _context = context;
        }

        public void Dispose()
        {

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}