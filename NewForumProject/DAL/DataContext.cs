using NewForumProject.Models;
using System.Data.Entity;

namespace NewForumProject.DAL
{
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserID");
                    m.MapRightKey("RoleID");
                });
            modelBuilder.Entity<User>()
               .HasMany(u => u.Subjects)
               .WithMany(r => r.Users)
               .Map(m =>
               {
                   m.ToTable("UserSubjects");
                   m.MapLeftKey("UserID");
                   m.MapRightKey("SubjectID");
               });
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Academy> Academies { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollAnswer> PollAnswers { get; set; }
        public DbSet<PollVote> PollVotes { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}