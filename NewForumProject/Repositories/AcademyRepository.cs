namespace NewForumProject.Repositories
{
    using NewForumProject.DAL;
    using NewForumProject.Interfaces;
    using NewForumProject.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AcademyRepository : IAcademyRepository
    {
        private readonly DataContext _dbContext;

        public AcademyRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Academy Get(int id)
        {
            return _dbContext.Academies.FirstOrDefault(x => x.AcademyID == id);
        }

        public void Save(Academy entity)
        {
            _dbContext.Academies.Attach(entity);
        }

        public void Delete(Academy entity)
        {
            _dbContext.Academies.Remove(entity);
        }

        public IEnumerable<Academy> FindAll()
        {
            return _dbContext.Academies.ToList();
        }

        public IEnumerable<Academy> Find(string text)
        {
            return _dbContext.Academies.Where(x => x.AcademyName.StartsWith(text)).ToList();
        }
    }
}